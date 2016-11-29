using System.Data.Common;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using ExpenseManager.Database.Entities;

[assembly: InternalsVisibleTo("ExpenseManager.Business.Tests")]
namespace ExpenseManager.Database
{
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext(DbConnection connection) : base(connection, true) { }

        public ExpenseDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public DbSet<BadgeModel> Badges { get; set; }

        public DbSet<CostInfoModel> CostInfos { get; set; }

        public DbSet<CostTypeModel> CostTypes { get; set; }

        public DbSet<PlanModel> Plans { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<AccountModel> Accounts { get; set; }

        public DbSet<AccountBadgeModel> AccountBadges { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BadgeModel>().ToTable("Badges");
            modelBuilder.Entity<CostInfoModel>().ToTable("CostInfos");
            modelBuilder.Entity<CostTypeModel>().ToTable("CostTypes");
            modelBuilder.Entity<PlanModel>().ToTable("Plans");
            modelBuilder.Entity<UserModel>().ToTable("Users");
            modelBuilder.Entity<AccountModel>().ToTable("Accounts");
            modelBuilder.Entity<AccountBadgeModel>().ToTable("AccountBadges");
        }
    }
}
