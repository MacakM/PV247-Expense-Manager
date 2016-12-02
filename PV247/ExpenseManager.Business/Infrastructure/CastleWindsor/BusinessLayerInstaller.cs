using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ExpenseManager.Business.Facades;
using ExpenseManager.Business.Infrastructure.Mapping.Profiles;
using ExpenseManager.Business.Services.Implementations;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Business.Utilities.BadgeCertification;
using ExpenseManager.Business.Utilities.BadgeCertification.BadgeCertifiers;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.ConnectionConfiguration;
using ExpenseManager.Database.Infrastructure.Query;
using ExpenseManager.Database.Infrastructure.Repository;
using ExpenseManager.Database.Infrastructure.UnitOfWork;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Infrastructure.CastleWindsor
{
    internal class BusinessLayerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DatabaseToBusinessStandardMapping>();
            });
            var mapper = config.CreateMapper();

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Register(

                Component.For<Mapper>()
                    .Instance(mapper as Mapper)
                    .LifestyleSingleton(),

                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<ExpenseManagerUnitOfWorkProvider>()
                    .LifestyleSingleton(),

                Component.For<IUnitOfWorkRegistry>()
                    .Instance(new HttpContextUnitOfWorkRegistry(new ThreadLocalUnitOfWorkRegistry()))
                    //.Named(nameof(HttpContextUnitOfWorkRegistry))
                    .LifestyleSingleton(),

                Classes.FromAssemblyContaining<ExpenseManagerUnitOfWork>()
                    .BasedOn(typeof(ExpenseManagerRepository<,>))
                    .Unless(type => type == typeof(ExpenseManagerRepository<UserModel, Guid>))
                    .LifestyleTransient(),

                Component.For<UserRepository>()
                    .ImplementedBy<UserRepository>()
                    .IsDefault()
                    .Named(Guid.NewGuid().ToString())
                    .LifestyleTransient(),

                Component.For<IBadgeCertifierResolver>()
                    .ImplementedBy<BadgeCertifierResolver>()
                    .LifestyleSingleton(),

                Classes.FromAssemblyContaining<IBadgeCertifierResolver>()
                    .BasedOn<IBadgeCertifier>()
                    .WithService.FromInterface()
                    .LifestyleSingleton(),

                Classes.FromAssemblyContaining<ExpenseManagerUnitOfWork>()
                    .BasedOn(typeof(ExpenseManagerQuery<>)).WithService.Base()
                    .LifestyleTransient()//,

                /*Classes.FromAssemblyContaining<IService>()                    
                    .BasedOn<IService>()                  
                    .Unless(type => type == typeof(IGraphService))
                    .WithServiceDefaultInterfaces()
                    .LifestyleTransient()*/


                
            );
            container.Register(RegisterManyBasedOn<IService>(container.Kernel));
            //container.Register(UsingFactoryMethod<IGraphService>(container.Kernel));
            
            // TODO make all ctors internal
        }

        private static IRegistration[] RegisterManyBasedOn<T>(IKernel containerKernel, bool registerInterfaces = true) where T : class
        {

            var typesToRegister = TypesFromCurrentAssembly
                .Where(type => registerInterfaces ? type.GetInterfaces().Contains(typeof (T)) && 
                        type.IsInterface : type.IsSubclassOf(typeof(T)))
                .ToList();
            var registrations = new IRegistration[typesToRegister.Count];

            for (var i = 0; i < typesToRegister.Count; i++)
            {
                var usingFactoryMethod = typeof(BusinessLayerInstaller)
                    .GetMethod(nameof(RegisterComponentFor), BindingFlags.NonPublic | BindingFlags.Static);
                var usingFactoryMethodRef = usingFactoryMethod.MakeGenericMethod(typesToRegister[i]);
                registrations[i] = usingFactoryMethodRef
                    .Invoke(new BusinessLayerInstaller(), new object[] { containerKernel }) as IRegistration;
            }
            return registrations;
        }

        private static IRegistration RegisterComponentFor<T>(IKernel containerKernel) where T : class
        {
            var typeToRegister = TypesFromCurrentAssembly
                                    .FirstOrDefault(type => type.GetInterfaces().Contains(typeof(T)) || type.IsSubclassOf(typeof(T)));
            if (typeToRegister == null)
            {
                throw new ArgumentNullException(nameof(typeToRegister));
            }

            var ctorParameters = typeToRegister.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                                                .First()
                                                .GetParameters();

            var ctorArgs = new object[ctorParameters.Length];
            for (var i = 0; i < ctorParameters.Length; i++)
            {
                ctorArgs[i] = containerKernel.Resolve(ctorParameters[i].ParameterType);
            }
           
            return Component.For<T>()
                .UsingFactoryMethod(kernel => Activator.CreateInstance(typeToRegister, BindingFlags.Instance | BindingFlags.NonPublic, null, ctorArgs, null, null) as T)
                .LifestyleTransient();
        }

        private static readonly Type[] TypesFromCurrentAssembly = Assembly.GetExecutingAssembly().GetTypes();
    }
}
