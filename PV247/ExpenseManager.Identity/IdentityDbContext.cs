using ExpenseManager.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Identity
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityDbContext() { }
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Connection string is required by SQL server
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=IdentityStoreDB;Integrated Security=True;MultipleActiveResultSets=true");
        }
    }
}
