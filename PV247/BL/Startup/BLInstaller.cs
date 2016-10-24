using System;
using System.Data.Entity;
using AutoMapper;
using BL.DTOs;
using BL.Facades;
using BL.Infrastructure.Queries;
using BL.Infrastructure.Services;
using BL.Infrastructure.UnitOfWork;
using BL.Queries;
using BL.Repositories;
using BL.Services;
using BL.Startup.Mapping.Profiles;
using DAL;
using DAL.Entities;
using Microsoft.Extensions.DependencyInjection;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Startup
{
    public static class BLInstaller
    {
        /// <summary>
        /// Performs BL DI configuration
        /// </summary>
        /// <param name="services">Collection of the service descriptions</param>
        public static void Install(IServiceCollection services)
        {
            Func<DbContext> dbContextFactory = () => new ExpenseDbContext();
            services.AddTransient(provider => dbContextFactory);

            services.AddSingleton<IUnitOfWorkRegistry>(new HttpContextUnitOfWorkRegistry(new ThreadLocalUnitOfWorkRegistry()));

            services.AddSingleton<IUnitOfWorkProvider, ExpenseManagerUnitOfWorkProvider>();

            services.AddTransient(typeof(IRepository<,>),typeof(EntityFrameworkRepository<,>));

            services.AddSingleton(typeof(Mapper), 
                provider => {
                    var config = new MapperConfiguration(cfg => 
                    {
                        cfg.AddProfile<StandardMappingProfile>();
                    });
                    return config.CreateMapper();
            });

            // Register all repositories
            services.AddTransient<IRepository<Badge, int>, BadgeRepository>();
            services.AddTransient<IRepository<CostInfo, int>, CostInfoRepository>();
            services.AddTransient<IRepository<CostInfoPaste, int>, CostInfoPasteRepository>();
            services.AddTransient<IRepository<CostType, int>, CostTypeRepository>();
            services.AddTransient<IRepository<Paste, int>, PasteRepository>();
            services.AddTransient<IRepository<Plan, int>, PlanRepository>();
            services.AddTransient<IRepository<User, int>, UserRepository>();
            services.AddTransient<IRepository<UserBadge, int>, UserBadgeRepository>();
            services.AddTransient<IRepository<UserPasteAccess, int>, UserPasteAccessRepository>();

            // Register all query objects
            services.AddTransient<ExpenseManagerQuery<PlanDTO>, ListUserPlansQuery>();
            //TODO add more query objects

            // Register all services
            services.AddTransient(typeof(ExpenseManagerCrudServiceBase<User,int,UserDTO>), typeof(UserService));
            services.AddTransient<IUserService, UserService>();
            //TODO add more services

            // Register all facades
            services.AddTransient<UserFacade>();
            services.AddTransient<PlanFacade>();
            //TODO add more facades
            
        }
    }
}
