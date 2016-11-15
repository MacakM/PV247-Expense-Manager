using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Facades;
using ExpenseManager.Business.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpenseManager.Database;

namespace ExpenseManager.Business.Tests.Facades
{
    [TestClass]
    public class BalanceFacadeTests
    {
        private IWindsorContainer container = new WindsorContainer();
        private readonly BalanceFacade _balanceFacade;

        public BalanceFacadeTests()
        {
            container.Install(new TestInstaller());
            _balanceFacade = container.Resolve<BalanceFacade>();
        }

        [TestMethod]
        public void ListAllCloseablePlans()
        {
            _balanceFacade.CreateBadge(new Badge() {Accounts = new List<AccountBadge>(), BadgeImgUri = "somePicture", Description = "Expense Manager badge", Name = "Penny Pincher"});
            var x = _balanceFacade.ListBages(new BadgeFilter());
            throw new AssertFailedException();
        }
        [TestMethod]
        public void CheckBadgesRequirements()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void RecomputePeriodicCosts()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void CreateItemTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void DeleteItemTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void UpdateItemTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void GetItemTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void ListItemTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void CreatePlanTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void DeletePlanTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void UpdatePlanTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void GetPlanTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void ListPlansTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void CreateItemTypeTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void DeleteItemTypeTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void UpdateItemTypeTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void GetItemTypeTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void ListItemTypesTest()
        {
            throw new AssertFailedException();
        }
        /// <summary>
        /// Test Badge creation.
        /// </summary>
        [TestMethod]
        public void CreateBadgeTest()
        { 
            _balanceFacade.CreateBadge(new Badge
            {
                Name = "Organizer",
                Description = "Add your first expense",
                BadgeImgUri = "lol"
            });
            using (var db = new ExpenseDbContext())
            {
                var myBadge = db.Badges.FirstOrDefault(model => model.Name.Equals("Organizer"));
                Assert.IsTrue(myBadge != null && myBadge.Description.Equals("Add your first expense") && myBadge.BadgeImgUri.Equals("lol"), "Badge was not created successfuly");
            }
        }
        /// <summary>
        /// Test Badge deletion.
        /// </summary>
        [TestMethod]
        public void DeleteBadgeTest()
        {
            _balanceFacade.DeleteBadge(2);
            using (var db = new ExpenseDbContext())
            {
                var myBadge = db.Badges.FirstOrDefault(model => model.Name.Equals("Survivor"));
                Assert.IsTrue(myBadge == null, "Badge was not deleted successfuly");
            }
        }
        /// <summary>
        /// Test Badge update.
        /// </summary>
        [TestMethod]
        public void UpdateBadgeTest()
        {
            _balanceFacade.UpdateBadge(new Badge
            {
                Id = 3,
                Name = "Officer",
                Description = "Buy 5 donuts",
                BadgeImgUri = "mmm"
            });

            using (var db = new ExpenseDbContext())
            {
                var myBadge = db.Badges.Find(3);
                Assert.IsTrue(myBadge.Description == "Buy 5 donuts", "Badge was not updated successfuly");
            }
        }
        /// <summary>
        /// Test Badge get.
        /// </summary>
        [TestMethod]
        public void GetBadgeTest()
        {
            var badge = _balanceFacade.GetBadge(3);
            Assert.IsTrue(
                badge.Name.Equals("Officer") && badge.Description.Equals("Buy donuts") &&
                badge.BadgeImgUri.Equals("mmm"), "Badge was not get successfuly");
        }
        [TestMethod]
        public void ListBadgesTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void CreateAccountBadgeTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void DeleteAccountBadgeTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void UpdateAccountBadgeTest()
        {
            throw new AssertFailedException();

        }
        [TestMethod]
        public void GetAccountBadgeTest()
        {
            throw new AssertFailedException();
        }
        [TestMethod]
        public void ListAccountBadgesTest()
        {
           throw new AssertFailedException();
        }
    
    }
}
