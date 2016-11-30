using ExpenseManager.Database;
using ExpenseManager.DataSeeding.Hangfire;
using ExpenseManager.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.DataSeeding
{
    /// <summary>
    /// This project should be launched before 
    /// the ExpenseManager.Presentation project start
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            InitializeIdentityStoreDb();
            PerformExpenseDbContextSeed();
            InitializeHangfireDb();
        }

        private static void InitializeIdentityStoreDb()
        {
            using (var identityDbContext = new IdentityDbContext())
            {
                identityDbContext.Database.Migrate();
            }
        }

        private static void PerformExpenseDbContextSeed()
        {
            using (var expenseDbContext = new ExpenseDbContext("Server=(localdb)\\mssqllocaldb;Database=ExpenseManagerDB;Trusted_Connection=True;MultipleActiveResultSets=true"))
            {
                new ExpenseDbInitializer().InitializeDatabase(expenseDbContext);
            }
        }

        private static void InitializeHangfireDb()
        {
            using (var hangfireDbContext = new HangfireDbContext())
            {
                // Just need to create hangfire db context here to ensure the DB gets created
            }
        }
    }
}
