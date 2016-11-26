using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Facades;
using ExpenseManager.Database;
using ExpenseManager.Database.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace ExpenseManager.Business.Tests.Facades
{
    [TestFixture]
    public class BalanceFacadeTests
    {
        private readonly BalanceFacade _balanceFacade = GlobalTestInitializer.Container.Resolve<BalanceFacade>();

        /// <summary>
        /// Performs db cleanup after every test
        /// </summary>
        [TearDown]
        public void PerformAfterTestCleanup()
        {
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Database.Initialize(true);
            }
        }

        [Test]
        public void ListAllCloseablePlans()
        {
            _balanceFacade.CreateBadge(new Badge() {Accounts = new List<AccountBadge>(), BadgeImgUri = "somePicture", Description = "Expense Manager badge", Name = "Penny Pincher"});
            var x = _balanceFacade.ListBages(new BadgeFilter());
            throw new AssertFailedException();
        }

        [Test]
        public void CheckBadgesRequirements()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void RecomputePeriodicCosts()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void CreateItemTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void DeleteItemTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void UpdateItemTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void GetItemTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void ListItemTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void CreatePlanTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void DeletePlanTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void UpdatePlanTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void GetPlanTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void ListPlansTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void CreateItemTypeTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void DeleteItemTypeTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void UpdateItemTypeTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void GetItemTypeTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void ListItemTypesTest()
        {
            throw new AssertFailedException();
        }

        /// <summary>
        /// Test Badge creation.
        /// </summary>
        [Test]
        public void CreateBadgeTest()
        { 
            _balanceFacade.CreateBadge(new Badge
            {
                Name = "Organizer",
                Description = "Add your first expense",
                BadgeImgUri = "lol"
            });
            using (var db = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                var myBadge = db.Badges.FirstOrDefault(model => model.Name.Equals("Organizer"));
                Assert.IsTrue(myBadge != null && myBadge.Description.Equals("Add your first expense") && myBadge.BadgeImgUri.Equals("lol"), "Badge was not created successfuly");
            }
        }

        /// <summary>
        /// Test Badge deletion.
        /// </summary>
        [Test]
        public void DeleteBadgeTest()
        {
            Guid id;
            using (var db = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                id = db.Badges.Max(b => b.Id);
            }
            _balanceFacade.DeleteBadge(id);
            using (var db = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                var myBadge = db.Badges.FirstOrDefault(model => model.Name.Equals("Survivor"));
                Assert.IsTrue(myBadge == null, "Badge was not deleted successfuly");
            }
            //cleanup
            var badge = new BadgeModel()
            {
                Name = "Survivor",
                BadgeImgUri = "hmm",
                Description = "I will survive"
            };
            using (var db = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Badges.Add(badge);
            }
        }

        /// <summary>
        /// Test Badge update.
        /// </summary>
        [Test]
        public void UpdateBadgeTest()
        {
            Guid id;
            using (var db = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                id = db.Badges.Min(b => b.Id);
            }
            _balanceFacade.UpdateBadge(new Badge
            {
                Id = id,
                Name = "Officer",
                Description = "Buy 5 donuts",
                BadgeImgUri = "mmm"
            });

            using (var db = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                var myBadge = db.Badges.Find(id);
                Assert.IsTrue(myBadge.Description == "Buy 5 donuts", "Badge was not updated successfuly");
            }
        }

        /// <summary>
        /// Test Badge get.
        /// </summary>
        [Test]
        public void GetBadgeTest()
        {
            Guid id;
            using (var db = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                id = db.Badges.Min(b => b.Id);
            }
            var badge = _balanceFacade.GetBadge(id);
            Assert.IsTrue(
                badge.Name.Equals("Officer") && badge.Description.Equals("Buy donuts") &&
                badge.BadgeImgUri.Equals("mmm"), "Badge was not get successfuly");
        }

        [Test]
        public void ListBadgesTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void CreateAccountBadgeTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void DeleteAccountBadgeTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void UpdateAccountBadgeTest()
        {
            throw new AssertFailedException();

        }

        [Test]
        public void GetAccountBadgeTest()
        {
            throw new AssertFailedException();
        }

        [Test]
        public void ListAccountBadgesTest()
        {
           throw new AssertFailedException();
        }
    }
}
