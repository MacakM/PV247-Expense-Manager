using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database
{
    internal class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext() : base("ExpenseManagerDB") { }

        public ExpenseDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public DbSet<Badge> Badges { get; set; }
        public DbSet<CostInfo> CostInfos { get; set; }
        public DbSet<CostType> CostTypes { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountBadge> AccountBadges { get; set; }
        
    }
}
