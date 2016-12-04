using System;
using System.Data.Entity;
using System.Reflection;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ExpenseManager.Business.Facades;
using ExpenseManager.Business.Infrastructure.CastleWindsor;
using ExpenseManager.Business.Infrastructure.Mapping.Profiles;
using ExpenseManager.Database;
using ExpenseManager.Database.Infrastructure.ConnectionConfiguration;
using ExpenseManager.Database.Infrastructure.UnitOfWork;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Tests.Bootstrap
{
    /// <summary>
    /// Installer for dependencies in tests.
    /// </summary>
    public class TestInstaller : IWindsorInstaller
    {
        internal const string ExpenseManagerTestDbConnection = "1";

        /// <summary>
        /// Configure dependencies for BL
        /// </summary>
        /// <param name="container">The IoC container</param>
        /// <param name="store">Provides a contract to obtain external configuration</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            BootstrapBusinessLayerIoCContainer();
            RegisterTestDependencies(container);
        }

        private static void BootstrapBusinessLayerIoCContainer()
        {
            Func<DbContext> dbContextFactory =
                () => new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(ExpenseManagerTestDbConnection));
            BusinessLayerInstaller.AddCustomComponent(Component.For<IUnitOfWorkProvider>()
                .Instance(Activator.CreateInstance(typeof (ExpenseManagerUnitOfWorkProvider),
                    BindingFlags.Instance | BindingFlags.NonPublic, null,
                    new object[]
                    {
                        dbContextFactory, new HttpContextUnitOfWorkRegistry(
                            new ThreadLocalUnitOfWorkRegistry())
                    }, null, null) as ExpenseManagerUnitOfWorkProvider)
                .Named(Guid.NewGuid().ToString())
                .LifestyleSingleton());
            BusinessLayerDIManager.BootstrapContainer(new ConnectionOptions());
        }

        private static void RegisterTestDependencies(IWindsorContainer container)
        {
            container.Register( 
                Component.For<Mapper>()
                    .Instance(new MapperConfiguration(cfg => 
                        { cfg.AddProfile<DatabaseToBusinessStandardMapping>(); })
                        .CreateMapper() as Mapper)
                    .LifestyleSingleton(),
                Component.For<AccountFacade>()
                .LifestyleTransient(),
                Component.For<ExpenseFacade>()
                .LifestyleTransient(),
                Component.For<BalanceFacade>()
                    .LifestyleTransient());              
        }
    }
}
