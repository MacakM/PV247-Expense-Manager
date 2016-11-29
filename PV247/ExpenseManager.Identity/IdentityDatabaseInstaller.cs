using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseManager.Identity
{
    /// <summary>
    /// Identity data access installer
    /// </summary>
    public static class IdentityDatabaseInstaller
    {
        /// <summary>
        /// Install method
        /// </summary>
        /// <param name="services">Services</param>
        /// <param name="connectionString">Connection string</param>
        public static void Install(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IdentityDbContext>(builder => 
            builder.UseSqlServer(
                connectionString, 
                sqlServerOptionsAction => 
                sqlServerOptionsAction.MigrationsAssembly("ExpenseManager.DataSeeding")))
                .BuildServiceProvider();
            services.AddIdentity<Entities.ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
