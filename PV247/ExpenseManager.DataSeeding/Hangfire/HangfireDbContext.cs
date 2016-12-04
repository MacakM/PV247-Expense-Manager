using System.Data.Entity;

namespace ExpenseManager.DataSeeding.Hangfire
{
    internal class HangfireDbContext : DbContext
    {
        internal HangfireDbContext() : base("Server=(localdb)\\MSSQLLocalDB;Database=HangFireDB;Integrated Security=True;MultipleActiveResultSets=true")
        {
            Database.CreateIfNotExists();
        }
    }
}
