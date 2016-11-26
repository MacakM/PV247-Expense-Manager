using ExpenseManager.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Identity
{
    /// <summary>
    /// Identity database context
    /// </summary>
    public class IdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Identity database context constructor
        /// </summary>
        public IdentityDbContext() { }

        /// <summary>
        /// Indetity database context constructor
        /// </summary>
        /// <param name="options">Options</param>
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

        /// <summary>
        /// On configuring method
        /// </summary>
        /// <param name="optionsBuilder">Options builder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Connection string is required by SQL server
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=IdentityStoreDB;Integrated Security=True;MultipleActiveResultSets=true");
        }
    }
}
