using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.Facades;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Infrastructure.Mapping.Profiles;
using ExpenseManager.Business.Services.Implementations;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.ConnectionConfiguration;
using ExpenseManager.Database.Infrastructure.Query;
using ExpenseManager.Database.Infrastructure.Repository;
using ExpenseManager.Database.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Options;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Tests
{
    /// <summary>
    /// Installer for dependencies in tests.
    /// </summary>
    public class TestInstaller : IWindsorInstaller
    {
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
                    .Instance(() => new ExpenseDbContext())
                    .LifestyleTransient(),

                Component.For<AccountFacade>()
                    .LifestyleTransient(),

                Component.For<IOptions<ConnectionOptions>>()
                    .Instance(new OptionsWrapper<ConnectionOptions>(
                        new ConnectionOptions
                        {
                            ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=ExpenseManagerDB;Trusted_Connection=True;MultipleActiveResultSets=true"
                        }))
                    .LifestyleSingleton(),

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
                    .LifestyleTransient(),

                Classes.FromAssemblyContaining<ExpenseManagerUnitOfWork>()
                    .BasedOn(typeof(ExpenseManagerQuery<,>)).WithService.Base()
                    .LifestyleTransient(),
 
                Classes.FromAssemblyContaining<IService>()
                    .BasedOn<IService>()
                    .WithServiceDefaultInterfaces()
                    .LifestyleTransient()
            );
        }
    }
}
