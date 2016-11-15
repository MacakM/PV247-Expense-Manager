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
        /// Instal dependencies
        /// </summary>
        /// <param name="container">container</param>
        /// <param name="store">store</param>
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
                .Instance(new OptionsWrapper<ConnectionOptions>(new ConnectionOptions{ConnectionString =
                            "Server=(localdb)\\mssqllocaldb;Database=ExpenseManagerDB;Trusted_Connection=True;MultipleActiveResultSets=true"}))
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

                /*Component.For(typeof(IRepository<,>))
                    .ImplementedBy(typeof(ExpenseManagerRepository<,>))
                    .LifestyleTransient(),

                // repositories
                Component.For(typeof(IRepository<,>))
                    .ImplementedBy(typeof(ExpenseManagerRepository<,>))
                    .LifestyleTransient(),
                */
                Component.For(typeof(ExpenseManagerRepository<BadgeModel, int>))
                    .ImplementedBy(typeof(BadgeRepository))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerRepository<CostTypeModel, int>))
                    .ImplementedBy(typeof(CostTypeRepository))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerRepository<PlanModel, int>))
                    .ImplementedBy(typeof(PlanRepository))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerRepository<UserModel, int>))
                    .ImplementedBy(typeof(UserRepository))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerRepository<AccountBadgeModel, int>))
                    .ImplementedBy(typeof(AccountBadgeRepository))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerRepository<AccountModel, int>))
                    .ImplementedBy(typeof(AccountRepository))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerRepository<CostInfoModel, int>))
                    .ImplementedBy(typeof(CostInfoRepository))
                    .LifestyleTransient(),
                    
                // query objects
                Component.For(typeof(ExpenseManagerQuery<AccountBadgeModel, AccountBadgeModelFilter>))
                    .ImplementedBy(typeof(ListAccountBadgesQuery))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerQuery<AccountModel, AccountModelFilter>))
                    .ImplementedBy(typeof(ListAccountsQuery))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerQuery<BadgeModel, BadgeModelFilter>))
                    .ImplementedBy(typeof(ListBadgesQuery))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerQuery<CostInfoModel, CostInfoModelFilter>))
                    .ImplementedBy(typeof(ListCostInfosQuery))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerQuery<CostTypeModel, CostTypeModelFilter>))
                    .ImplementedBy(typeof(ListCostTypesQuery))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerQuery<PlanModel, PlanModelFilter>))
                    .ImplementedBy(typeof(ListPlansQuery))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerQuery<UserModel, UserModelFilter>))
                    .ImplementedBy(typeof(ListUsersQuery))
                    .LifestyleTransient(),

                //services
                Classes.FromAssemblyContaining<IService>()
                    .BasedOn<IService>()
                    .WithServiceDefaultInterfaces()
                    .LifestyleTransient()


                /*Component.For(typeof(ExpenseManagerQueryAndCrudServiceBase<AccountBadgeModel, int, AccountBadge, AccountBadgeModelFilter>))
                    .ImplementedBy(typeof(AccountBadgeService))
                    .LifestyleTransient(),
                Component.For<IAccountBadgeService>()
                    .ImplementedBy(typeof(AccountBadgeService))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerQueryAndCrudServiceBase<AccountModel, int, Account, AccountModelFilter>))
                    .ImplementedBy(typeof(AccountService))
                    .LifestyleTransient(),
                Component.For<IAccountService>()
                    .ImplementedBy(typeof(AccountService))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerQueryAndCrudServiceBase<BadgeModel, int, Badge, BadgeModelFilter>))
                    .ImplementedBy(typeof(BadgeService))
                    .LifestyleTransient(),
                Component.For<IBadgeService>()
                    .ImplementedBy(typeof(BadgeService))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerQueryAndCrudServiceBase<CostInfoModel, int, CostInfo, CostInfoModelFilter>))
                    .ImplementedBy(typeof(CostInfoService))
                    .LifestyleTransient(),
                Component.For<ICostInfoService>()
                    .ImplementedBy(typeof(CostInfoService))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerQueryAndCrudServiceBase<CostTypeModel, int, CostType, CostTypeModelFilter>))
                    .ImplementedBy(typeof(CostTypeService))
                    .LifestyleTransient(),
                Component.For<ICostTypeService>()
                    .ImplementedBy(typeof(CostTypeService))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerQueryAndCrudServiceBase<PlanModel, int, Plan, PlanModelFilter>))
                    .ImplementedBy(typeof(PlanService))
                    .LifestyleTransient(),
                Component.For<IPlanService>()
                    .ImplementedBy(typeof(PlanService))
                    .LifestyleTransient(),
                Component.For(typeof(ExpenseManagerQueryAndCrudServiceBase<UserModel, int, User, UserModelFilter>))
                    .ImplementedBy(typeof(UserService))
                    .LifestyleTransient(),
                Component.For<IUserService>()
                    .ImplementedBy(typeof(UserService))
                    .LifestyleTransient(),
                Component.For<IBadgeManagerService>()
                    .ImplementedBy(typeof(BadgeManagerService))
                    .LifestyleTransient()*/
            );
        }
    }
}
