using System.Data.Common;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using ExpenseManager.Database.Entities;

[assembly: InternalsVisibleTo("ExpenseManager.Business.Tests")]
namespace ExpenseManager.Database
{
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext(DbConnection connection) : base(connection, true)
        {
            System.Data.Entity.Database.SetInitializer(new ExpenseDbInitializer());
        }

        public ExpenseDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            System.Data.Entity.Database.SetInitializer(new ExpenseDbInitializer());
        }

        public DbSet<BadgeModel> Badges { get; set; }

        public DbSet<CostInfoModel> CostInfos { get; set; }

        public DbSet<CostTypeModel> CostTypes { get; set; }

        public DbSet<PlanModel> Plans { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<AccountModel> Accounts { get; set; }

        public DbSet<AccountBadgeModel> AccountBadges { get; set; }
    }
}
