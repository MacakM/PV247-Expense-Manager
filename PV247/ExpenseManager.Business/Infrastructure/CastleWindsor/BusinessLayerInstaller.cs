using System;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ExpenseManager.Business.Infrastructure.Mapping.Profiles;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Business.Utilities.BadgeCertification;
using ExpenseManager.Business.Utilities.BadgeCertification.BadgeCertifiers;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Infrastructure.Query;
using ExpenseManager.Database.Infrastructure.Repository;
using ExpenseManager.Database.Infrastructure.UnitOfWork;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Infrastructure.CastleWindsor
{
    internal class BusinessLayerInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Installer for all business layer dependencies
        /// </summary>
        /// <param name="container">The IoC container to register dependencies to</param>
        /// <param name="store">Contract for storing information used in windsor kernel</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            RegisterThirdPartyDependencies(container);
            RegisterAllRepositories(container);
            RegisterAllQueryObjects(container);
            RegisterBadgeCertification(container);
            RegisterAllServices(container);
        }

        private static void RegisterAllServices(IWindsorContainer container)
        {
            container.Register(container.Kernel.RegisterManyWithDefaultInterfacesBasedOnInterface<IService>());
        }

        private static void RegisterBadgeCertification(IWindsorContainer container)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.Register(container.Kernel.RegisterManyBasedOnInterface<IBadgeCertifier>(true));
            container.Register(
                container.Kernel
                    .RegisterComponentWithCollectionFor<IBadgeCertifierResolver, BadgeCertifierResolver, IBadgeCertifier>(true));
        }

        private static void RegisterAllQueryObjects(IWindsorContainer container)
        {
            container.Register(container.Kernel.RegisterManyBasedOn(typeof (ExpenseManagerQuery<>)));
        }

        private static void RegisterAllRepositories(IWindsorContainer container)
        {
            container.Register(container.Kernel.RegisterManyBasedOn(typeof (ExpenseManagerRepository<,>)));
            container.Register(Component.For<UserRepository>()
                .ImplementedBy<UserRepository>()
                .IsDefault()
                .Named(Guid.NewGuid().ToString())
                .LifestyleTransient());
        }

        private static void RegisterThirdPartyDependencies(IWindsorContainer container)
        {
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile<DatabaseToBusinessStandardMapping>(); }).CreateMapper();
            container.Register(
                Component.For<Mapper>()
                    .Instance(mapper as Mapper)
                    .LifestyleSingleton(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<ExpenseManagerUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For<IUnitOfWorkRegistry>()
                    .Instance(new HttpContextUnitOfWorkRegistry(new ThreadLocalUnitOfWorkRegistry()))
                    .LifestyleSingleton()
                );
        }
    }
}
