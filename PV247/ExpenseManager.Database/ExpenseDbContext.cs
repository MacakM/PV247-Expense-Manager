using System.Data.Common;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using ExpenseManager.Database.Entities;

[assembly: InternalsVisibleTo("ExpenseManager.Business.Tests")]
[assembly: InternalsVisibleTo("ExpenseManager.DataSeeding")]
namespace ExpenseManager.Database
{
    /// <summary>
    /// Database context
    /// </summary>
    internal class ExpenseDbContext : DbContext
    {
        /// <summary>
        /// Context construstor
        /// </summary>
        /// <param name="connection"></param>
        public ExpenseDbContext(DbConnection connection) : base(connection, true) { }

        /// <summary>
        /// Context constructor
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public ExpenseDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ExpenseDbContext>());
        }

        /// <summary>
        /// Badges DbSet
        /// </summary>
        public DbSet<BadgeModel> Badges { get; set; }

        /// <summary>
        /// CostInfos DbSet
        /// </summary>
        public DbSet<CostInfoModel> CostInfos { get; set; }

        /// <summary>
        /// CostTypes DbSet
        /// </summary>
        public DbSet<CostTypeModel> CostTypes { get; set; }

        /// <summary>
        /// Plans DbSet
        /// </summary>
        public DbSet<PlanModel> Plans { get; set; }

        /// <summary>
        /// Users DbSet
        /// </summary>
        public DbSet<UserModel> Users { get; set; }

        /// <summary>
        /// Accounts DbSet
        /// </summary>
        public DbSet<AccountModel> Accounts { get; set; }

        /// <summary>
        /// AccountBadges DbSet
        /// </summary>
        public DbSet<AccountBadgeModel> AccountBadges { get; set; }

        /// <summary>
        /// Method is called when model is being created
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CostTypeModel>()
                .HasRequired(c => c.Account)
                .WithMany()
                .WillCascadeOnDelete(false);

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
