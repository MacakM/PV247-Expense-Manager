using System;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ExpenseManager.Business.Facades;
using ExpenseManager.Business.Infrastructure.Mapping.Profiles;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Business.Utilities.BadgeCertification;
using ExpenseManager.Business.Utilities.BadgeCertification.BadgeCertifiers;
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

                Component.For<AccountFacade>()
                    .LifestyleTransient(),

                Component.For<Mapper>()
                    .Instance(mapper as Mapper)
                    .LifestyleSingleton(),

                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<ExpenseManagerUnitOfWorkProvider>()
                    //.DependsOn(Dependency.OnComponent<ConnectionOptions, ConnectionOptions>(), 
                       // Dependency.OnComponent(typeof(IUnitOfWorkRegistry), nameof(HttpContextUnitOfWorkRegistry)))
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
                    .LifestyleTransient(),

                Classes.FromAssemblyContaining<IService>()
                    .BasedOn<IService>()
                    .WithServiceDefaultInterfaces()
                    .LifestyleTransient()
            );
        }
    }
}
