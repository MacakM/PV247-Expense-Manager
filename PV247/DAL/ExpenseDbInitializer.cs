using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
