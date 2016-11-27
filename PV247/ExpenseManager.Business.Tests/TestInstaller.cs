using System;
using System.Data.Entity;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ExpenseManager.Business.Facades;
using ExpenseManager.Business.Infrastructure.Mapping.Profiles;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Query;
using ExpenseManager.Database.Infrastructure.Repository;
using ExpenseManager.Database.Infrastructure.UnitOfWork;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Tests
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
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DatabaseToBusinessStandardMapping>();
            });
            var mapper = config.CreateMapper();

            container.Register(

                Component.For<Func<DbContext>>()
                    .Instance(() => new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(ExpenseManagerTestDbConnection)))
                    .LifestyleTransient(),

                Component.For<AccountFacade>()
                    .LifestyleTransient(),

                Component.For<Mapper>()
                    .Instance(mapper as Mapper)
                    .LifestyleSingleton(),
                
                Component.For<BalanceFacade>()
                    .LifestyleTransient(),

                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<ExpenseManagerUnitOfWorkProvider>()
                    .LifestyleSingleton(),

                Component.For<IUnitOfWorkRegistry>()
                    .Instance(new HttpContextUnitOfWorkRegistry(new ThreadLocalUnitOfWorkRegistry()))
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
