using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityDAL
{
    public static class IdentityDALInstaller
    {
        public static void Install(IServiceCollection services)
        {
            services.AddDbContext<IdentityDAL.IdentityDbContext>();

            services.AddIdentity<IdentityDAL.Entities.ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDAL.IdentityDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
