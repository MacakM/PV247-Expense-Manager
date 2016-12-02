using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ExpenseManager.Business.Infrastructure.Mapping.Profiles;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Business.Utilities.BadgeCertification;
using ExpenseManager.Business.Utilities.BadgeCertification.BadgeCertifiers;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
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
                    .LifestyleSingleton(),
                /*
                Classes.FromAssemblyContaining<ExpenseManagerUnitOfWork>()
                    .BasedOn(typeof(ExpenseManagerRepository<,>))                    
                    .LifestyleTransient(),
                */
                Component.For<UserRepository>()
                    .ImplementedBy<UserRepository>()
                    .IsDefault()
                    .Named(Guid.NewGuid().ToString())
                    .LifestyleTransient(),

                Classes.FromAssemblyContaining<ExpenseManagerUnitOfWork>()
                    .BasedOn(typeof(ExpenseManagerQuery<>)).WithService.Base()
                    .LifestyleTransient()//,            
            );

            container.Register(container.Kernel.RegisterManyBasedOn(typeof(ExpenseManagerRepository<,>)));

            container.Register(container.Kernel.RegisterManyBasedOnInterface<IBadgeCertifier>(true));
            container.Register(container.Kernel.RegisterComponentWithCollectionFor<IBadgeCertifierResolver, BadgeCertifierResolver, IBadgeCertifier>(true));


            container.Register(container.Kernel.RegisterManyWithDefaultInterfacesBasedOnInterface<IService>());

            //container.Register(UsingFactoryMethod<IGraphService>(container.Kernel));

            // TODO make all ctors internal
        }
    }
}
