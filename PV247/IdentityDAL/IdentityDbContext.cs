using IdentityDAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityDAL
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Connection string is required by SQL server
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=IdentityStoreDB;Integrated Security=True;MultipleActiveResultSets=true");
        }
    }
}
