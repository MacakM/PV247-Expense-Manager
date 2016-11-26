using System;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.Facades;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Infrastructure.Mapping.Profiles;
using ExpenseManager.Business.Services.Implementations;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.ConnectionConfiguration;
using ExpenseManager.Database.Infrastructure.Query;
using ExpenseManager.Database.Infrastructure.Repository;
using ExpenseManager.Database.Infrastructure.UnitOfWork;
using ExpenseManager.Identity;
using ExpenseManager.Presentation.Authentication;
using ExpenseManager.Presentation.Infrastructure.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Presentation
{
    /// <summary>
    /// Configuration at startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {  
            services.Configure<ConnectionOptions>(options => options.ConnectionString = Configuration.GetConnectionString("DefaultConnection"));

            // Configure Identity persistence         
            IdentityDatabaseInstaller.Install(services, Configuration.GetConnectionString("IdentityConnection"));

            // Configure BL
            RegisterBusinessLayerDependencies(services);

            // Configure PL
            services.AddSession();

            services.AddSingleton<IAuthorizationHandler, HasAccountHandler>();
            services.AddSingleton<IAuthorizationHandler, HasAccessRightsHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasAccount",
                                  policy => policy.Requirements.Add(new HasAccountRequirement()));
                options.AddPolicy("HasFullRights",
                                  policy => policy.Requirements.Add(new HasAccessRightsRequirement(AccountAccessType.Full)));
            });
            services.AddMvc(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSession();

            app.UseStaticFiles();

            app.UseIdentity();

            var x = Configuration.GetSection("FacebookAuthentication")["ClientId"];

            app.UseFacebookAuthentication(new FacebookOptions
            {
                ClientId = Configuration.GetSection("FacebookAuthentication")["ClientId"],
                ClientSecret = Configuration.GetSection("FacebookAuthentication")["ClientSecret"]
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Expense}/{action=Index}/{id?}");
            });
        }

        /// <summary>
        /// Performs BL DI configuration
        /// </summary>
        /// <param name="services">Collection of the service descriptions</param>
        private static void RegisterBusinessLayerDependencies(IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWorkRegistry>(new HttpContextUnitOfWorkRegistry(new ThreadLocalUnitOfWorkRegistry()));

            services.AddSingleton<IUnitOfWorkProvider, ExpenseManagerUnitOfWorkProvider>();

            services.AddTransient(typeof(IRepository<,>),typeof(ExpenseManagerRepository<,>));

            services.AddSingleton(typeof(Mapper), 
                provider => {
                    var config = new MapperConfiguration(cfg => 
                    {
                        cfg.AddProfile<DatabaseToBusinessStandardMapping>();
                        cfg.AddProfile<BussinessToViewModelMapping>();
                    });
                    return config.CreateMapper();
            });

            // Register all repositories
            services.AddTransient<ExpenseManagerRepository<BadgeModel, Guid>, BadgeRepository>();
            services.AddTransient<BadgeRepository>();
            services.AddTransient<ExpenseManagerRepository<CostInfoModel, Guid>, CostInfoRepository>();
            services.AddTransient<CostInfoRepository>();
            services.AddTransient<ExpenseManagerRepository<CostTypeModel, Guid>, CostTypeRepository>();
            services.AddTransient<CostTypeRepository>();
            services.AddTransient<ExpenseManagerRepository<PlanModel, Guid>, PlanRepository>();
            services.AddTransient<PlanRepository>();
            services.AddTransient<ExpenseManagerRepository<UserModel, Guid>, UserRepository>();
            services.AddTransient<UserRepository>();
            services.AddTransient<ExpenseManagerRepository<AccountBadgeModel, Guid>, AccountBadgeRepository>();
            services.AddTransient<AccountBadgeRepository>();
            services.AddTransient<ExpenseManagerRepository<AccountModel, Guid>, AccountRepository>();
            services.AddTransient<AccountRepository>();

            // Register all query objects
            services.AddTransient<ExpenseManagerQuery<AccountBadgeModel>, ListAccountBadgesQuery>();
            services.AddTransient<ListAccountBadgesQuery>();
            services.AddTransient<ExpenseManagerQuery<AccountModel>, ListAccountsQuery>();
            services.AddTransient<ListAccountsQuery>();
            services.AddTransient<ExpenseManagerQuery<BadgeModel>, ListBadgesQuery>();
            services.AddTransient<ListBadgesQuery>();
            services.AddTransient<ExpenseManagerQuery<CostInfoModel>, ListCostInfosQuery>();
            services.AddTransient<ListCostInfosQuery>();
            services.AddTransient<ExpenseManagerQuery<CostTypeModel>, ListCostTypesQuery>();
            services.AddTransient<ListCostTypesQuery>();
            services.AddTransient<ExpenseManagerQuery<PlanModel>, ListPlansQuery>();
            services.AddTransient<ListPlansQuery>();
            services.AddTransient<ExpenseManagerQuery<UserModel>, ListUsersQuery>();
            services.AddTransient<ListUsersQuery>();
            //TODO add more query objects

            // Register all services
            services.AddTransient(typeof(ExpenseManagerQueryAndCrudServiceBase<AccountBadgeModel, Guid, AccountBadge>), typeof(AccountBadgeService));
            services.AddTransient<IAccountBadgeService, AccountBadgeService>();

            services.AddTransient(typeof(ExpenseManagerQueryAndCrudServiceBase<AccountModel, Guid, Account>), typeof(AccountService));
            services.AddTransient<IAccountService, AccountService>();

            services.AddTransient(typeof(ExpenseManagerQueryAndCrudServiceBase<BadgeModel, Guid, Badge>), typeof(BadgeService));
            services.AddTransient<IBadgeService, BadgeService>();

            services.AddTransient(typeof(ExpenseManagerQueryAndCrudServiceBase<CostInfoModel, Guid, CostInfo>), typeof(CostInfoService));
            services.AddTransient<ICostInfoService, CostInfoService>();

            services.AddTransient(typeof(ExpenseManagerQueryAndCrudServiceBase<CostTypeModel, Guid, CostType>), typeof(CostTypeService));
            services.AddTransient<ICostTypeService, CostTypeService>();

            services.AddTransient(typeof(ExpenseManagerQueryAndCrudServiceBase<PlanModel, Guid, Plan>), typeof(PlanService));
            services.AddTransient<IPlanService, PlanService>();

            services.AddTransient(typeof(ExpenseManagerQueryAndCrudServiceBase<UserModel, Guid, User>), typeof(UserService));
            services.AddTransient<IUserService, UserService>();
            
            services.AddTransient<IBadgeManagerService, BadgeManagerService>();
            //TODO add more services

            // Register all facades
            services.AddTransient<AccountFacade>();
            services.AddTransient<BalanceFacade>();
            //TODO add more facades

            // Presentation layer
            services.AddTransient<ICurrentAccountProvider, CurrentAccountProvider>();

        }
    }
}
