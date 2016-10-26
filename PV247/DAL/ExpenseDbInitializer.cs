using System.Data.Entity;
using DAL.Entities;

namespace DAL
{
    class ExpenseDbInitializer : DropCreateDatabaseAlways<ExpenseDbContext>
    {
        public override void InitializeDatabase(ExpenseDbContext context)
        {
            context.Users.Add(new User {Name = "Peter"});
            base.InitializeDatabase(context);
        }
    }
}
