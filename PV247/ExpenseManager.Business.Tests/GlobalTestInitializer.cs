using System.Data.Entity;
using Castle.Windsor;
using ExpenseManager.Database;
using NUnit.Framework;

namespace ExpenseManager.Business.Tests
{
    /// <summary>
    /// Main initializer class for all Business Layer tests
    /// </summary>
    [SetUpFixture]
    public class GlobalTestInitializer
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer();

        /// <summary>
        /// Initializes all Business Layer tests
        /// </summary>
        [OneTimeSetUp]
        public void InitializeBusinessLayerTests()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();

            System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseAlways<ExpenseDbContext>());

            Container.Install(new TestInstaller());
        }
    }
}
