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
        /// Tests Badge creation.
        /// </summary>
        [Test]
        public void CreateBadgeTest()
        {
            // Arrange
            const string badgeName = "Organizer";
            var badge = new Badge
            {
                Name = badgeName,
                Description = "Add your first expense",
                BadgeImgUri = "picture"
            };

            // Act
            _balanceFacade.CreateBadge(badge);

            // Assert
            var createdBadge = GetBadgeByName(badgeName);
            Assert.That(createdBadge != null, "Badge was not created.");
        }
        /// <summary>
        /// Tests Badge deletion.
        /// </summary>
        [Test]
        public void DeleteBadgeTest()
        {
            // Arrange
            const string badgeName = "Organizer";
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Badges.Add(new BadgeModel()
                {
                    Name = badgeName,
                    Description = "Add your first expense",
                    BadgeImgUri = "picture"
                });
                dbContext.SaveChanges();
            }

            // Act
            _balanceFacade.DeleteBadge(1);

            // Assert
            var deletedBadge = GetBadgeByName(badgeName);
            Assert.That(deletedBadge == null, "Badge was not deleted.");
        }
        /// <summary>
        /// Tests Badge update.
        /// </summary>
        [Test]
        public void UpdateBadgeTest()
        {
            // Arrange
            const string badgeName1 = "Organizer";
            const string badgeName2 = "Survivor";
            var editedBadge = new Badge
            {
                Name = badgeName2,
                BadgeImgUri = "picture",
                Description = "I will survive"
            };
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Badges.Add(new BadgeModel()
                {
                    Name = badgeName1,
                    Description = "Add your first expense",
                    BadgeImgUri = "picture"
                });
                dbContext.SaveChanges();
            }

            // Act
            _balanceFacade.UpdateBadge(editedBadge);

            // Assert
            var updatedBadge = GetBadgeByName(badgeName2);
            Assert.That(updatedBadge != null, "Badge was not updated.");
        }
        /// <summary>
        /// Tests Badge get.
        /// </summary>
        [Test]
        public void GetBadgeTest()
        {
            // Arrange
            const string badgeName = "Organizer";
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Badges.Add(new BadgeModel()
                {
                    Name = badgeName,
                    Description = "Add your first expense",
                    BadgeImgUri = "picture"
                });
                dbContext.SaveChanges();
            }

            // Act
            var badge = _balanceFacade.GetBadge(1);

            // Assert
            Assert.That(badge.Name == badgeName, "Badge was not got.");
        }
        /// <summary>
        /// Tests whether listing badges works.
        /// </summary>
        [Test]
        public void ListBadgesTest1()
        {
            // Arrange
            const string badgeName1 = "Organizer";
            const string badgeName2 = "Survivor";
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Badges.Add(new BadgeModel()
                {
                    Name = badgeName1,
                    Description = "Add your first expense",
                    BadgeImgUri = "picture"
                });
                dbContext.Badges.Add(new BadgeModel()
                {
                    Name = badgeName2,
                    Description = "I will survive",
                    BadgeImgUri = "picture"
                });
                dbContext.SaveChanges();
            }

            // Act
            var badges = _balanceFacade.ListBages(new BadgeFilter());

            // Assert
            Assert.That(badges.Count == 2, "Badges were not listed.");
        }
        /// <summary>
        /// Tests whether BadgeFilter works.
        /// </summary>
        [Test]
        public void ListBadgesTest2()
        {
            // Arrange
            const string badgeName1 = "Organizer";
            const string badgeName2 = "Survivor";
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Badges.Add(new BadgeModel()
                {
                    Name = badgeName1,
                    Description = "Add your first expense",
                    BadgeImgUri = "picture"
                });
                dbContext.Badges.Add(new BadgeModel()
                {
                    Name = badgeName2,
                    Description = "I will survive",
                    BadgeImgUri = "picture"
                });
                dbContext.SaveChanges();
            }

            // Act
            var badges = _balanceFacade.ListBages(new BadgeFilter {Name = badgeName1});

            // Assert
            Assert.That(badges.Count == 1, "Badge was not listed.");
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
        private static BadgeModel GetBadgeByName(string badgeName)
        {
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                return db.Badges.FirstOrDefault(model => model.Name.Equals(badgeName));
            }
        }
    }
}
