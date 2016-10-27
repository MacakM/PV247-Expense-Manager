using APILayer.DTOs;
using AutoMapper;
using BL.Facades;
using BL.Infrastructure;
using BL.Services;
using DAL;
using DAL.DataAccess.Queries;
using DAL.DataAccess.Repositories;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Infrastructure.ConnectionConfiguration;
using DAL.Infrastructure.Mapping.Profiles;
using IdentityDAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Riganti.Utils.Infrastructure.Core;

namespace PL
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
            IdentityDALInstaller.Install(services);

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

            app.UseFacebookAuthentication(new FacebookOptions
            {
                ClientId = Configuration["Authentication:Facebook:AppId"],
                ClientSecret = Configuration["Authentication:Facebook:AppSecret"]
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
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

            services.AddTransient(typeof(IRepository<,>),typeof(ExpenseManagerRepository<,,>));

            services.AddSingleton(typeof(Mapper), 
                provider => {
                    var config = new MapperConfiguration(cfg => 
                    {
                        cfg.AddProfile<StandardMappingProfile>();
                    });
                    return config.CreateMapper();
            });

            // Register all repositories
            services.AddTransient<IRepository<Badge, BadgeDTO, int>, BadgeRepository>();
            services.AddTransient<IRepository<CostInfo, CostInfoDTO, int>, CostInfoRepository>();
            services.AddTransient<IRepository<CostInfoPaste, CostInfoPasteDTO, int>, CostInfoPasteRepository>();
            services.AddTransient<IRepository<CostType, CostTypeDTO, int>, CostTypeRepository>();
            services.AddTransient<IRepository<Paste, PasteDTO, int>, PasteRepository>();
            services.AddTransient<IRepository<Plan, PlanDTO, int>, PlanRepository>();
            services.AddTransient<IRepository<User, UserDTO, int>, UserRepository>();
            services.AddTransient<IRepository<UserBadge, UserBadgeDTO, int>, UserBadgeRepository>();
            services.AddTransient<IRepository<UserPasteAccess, UserPasteAccessDTO, int>, UserPasteAccessRepository>();

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
