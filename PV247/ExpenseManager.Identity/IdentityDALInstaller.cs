using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseManager.Identity
{
    public static class IdentityDALInstaller
    {
        public static void Install(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IdentityDbContext>(builder => builder.UseSqlServer(connectionString)).BuildServiceProvider();

            services.AddIdentity<Entities.ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
