using AutoMapper;
using ExpenseManager.Business.DTOs;
using ExpenseManager.Business.Facades;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Infrastructure.Mapping.Profiles;
using ExpenseManager.Business.Services;
using ExpenseManager.Business.Services.Implementations;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.ConnectionConfiguration;
using ExpenseManager.Database.Infrastructure.Query;
using ExpenseManager.Database.Infrastructure.Repository;
using ExpenseManager.Database.Infrastructure.UnitOfWork;
using ExpenseManager.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Presentation
{
    public class Startup
    {
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {  
            services.Configure<ConnectionOptions>(options => options.ConnectionString = Configuration.GetConnectionString("DefaultConnection"));

            // Configure Identity persistence         
            IdentityDALInstaller.Install(services, Configuration.GetConnectionString("IdentityConnection"));

            // Configure BL
            RegisterBusinessLayerDependencies(services);

            // Configure PL
            services.AddMvc(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                        cfg.AddProfile<StandardMappingProfile>();
                    });
                    return config.CreateMapper();
            });

            // Register all repositories
            services.AddTransient<ExpenseManagerRepository<Badge, int>, BadgeRepository>();
            services.AddTransient<ExpenseManagerRepository<CostInfo, int>, CostInfoRepository>();
            services.AddTransient<ExpenseManagerRepository<CostType, int>, CostTypeRepository>();
            services.AddTransient<ExpenseManagerRepository<Plan, int>, PlanRepository>();
            services.AddTransient<ExpenseManagerRepository<User, int>, UserRepository>();
            services.AddTransient<ExpenseManagerRepository<AccountBadge, int>, AccountBadgeRepository>();
            
            // Register all query objects
            services.AddTransient<ExpenseManagerQuery<Plan>, ListUserPlansQuery>();
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
