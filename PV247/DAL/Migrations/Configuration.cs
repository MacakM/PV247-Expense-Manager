using DAL.Entities;

namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.ExpenseDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ExpenseDbContext context)
        {
            context.Users.AddOrUpdate(new User { Name = "Demo user", Email = "demo@demo.com"});
        }
    }
}
