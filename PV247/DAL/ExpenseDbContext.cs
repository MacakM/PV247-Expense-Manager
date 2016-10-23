using System.Data.Entity;
using DAL.Entities;

namespace DAL
{
    public class ExpenseDbContext : DbContext
    {
        public DbSet<CostInfo> CostInfo { get; set; }
        public DbSet<CostType> CostTypes { get; set; }
        public DbSet<Paste> Pastes { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
