using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseManager.Identity
{
    public static class IdentityDALInstaller
    {
        public const string IdentityConnectionStringName = "IdentityConnection";

        public static void Install(IServiceCollection services, IConfigurationRoot config)
        {
            services.AddDbContext<IdentityDbContext>(builder => builder.UseSqlServer(config.GetConnectionString(IdentityConnectionStringName))).BuildServiceProvider();

            services.AddIdentity<Entities.ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
