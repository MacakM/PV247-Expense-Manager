using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
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
        /// <summary>
        /// Tests CostInfo creation.
        /// </summary>
        [Test]
        public void CreateItemTest()
        {
            // Arrange
            const string accountName = "ExpenseManagerAccount01";
            const string typeName = "Food";
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(new AccountModel
                {
                    Badges = new List<AccountBadgeModel>(),
                    Costs = new List<CostInfoModel>(),
                    Name = accountName
                });
                db.CostTypes.Add(new CostTypeModel
                {
                    Name = typeName,
                    CostInfoList = new EditableList<CostInfoModel>()
                });
                db.SaveChanges();
            }

            var item = new CostInfo()
            {
                Description = "bread",
                AccountId = 1,
                TypeId = 1,
                IsIncome = true,
                Money = 25,
                Created = DateTime.Now
            };

            // Act
            _balanceFacade.CreateItem(item);

            // Assert
            var createdItem = GetItemById(1);
            Assert.That(createdItem != null, "Item was not created.");
        }
        /// <summary>
        /// Test CostInfo deletion.
        /// </summary>
        [Test]
        public void DeleteItemTest()
        {
            // Arrange
            const string accountName = "ExpenseManagerAccount01";
            const string typeName = "Food";
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(new AccountModel
                {
                    Badges = new List<AccountBadgeModel>(),
                    Costs = new List<CostInfoModel>(),
                    Name = accountName
                });
                db.CostTypes.Add(new CostTypeModel
                {
                    Name = typeName,
                    CostInfoList = new EditableList<CostInfoModel>()
                });
                db.SaveChanges();
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "bread",
                    AccountId = 1,
                    TypeId = 1,
                    IsIncome = true,
                    Money = 25,
                    Created = DateTime.Now
                });
                db.SaveChanges();
            }

            // Act
            _balanceFacade.DeleteItem(1);

            // Assert
            var deletedItem = GetItemById(1);
            Assert.That(deletedItem == null, "Item was not deleted.");
        }
        /// <summary>
        /// Tests CostInfo update.
        /// </summary>
        [Test]
        public void UpdateItemTest()
        {
            // Arrange
            const string accountName = "ExpenseManagerAccount01";
            const string typeName = "Food";
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(new AccountModel
                {
                    Badges = new List<AccountBadgeModel>(),
                    Costs = new List<CostInfoModel>(),
                    Name = accountName
                });
                db.CostTypes.Add(new CostTypeModel
                {
                    Name = typeName,
                    CostInfoList = new EditableList<CostInfoModel>()
                });
                db.SaveChanges();
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "bread",
                    AccountId = 1,
                    TypeId = 1,
                    IsIncome = true,
                    Money = 25,
                    Created = DateTime.Now
                });
                db.SaveChanges();
            }

            // Act
            _balanceFacade.UpdateItem(new CostInfo
            {
                Id = 1,
                Description = "bread",
                AccountId = 1,
                TypeId = 1,
                IsIncome = true,
                Money = 50,
                Created = DateTime.Now
            });

            // Assert
            Assert.That(GetItemById(1).Money == 50, "Item was not updated.");
        }
        /// <summary>
        /// Tests get of CostInfo.
        /// </summary>
        [Test]
        public void GetItemTest()
        {
            // Arrange
            const string accountName = "ExpenseManagerAccount01";
            const string typeName = "Food";
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(new AccountModel
                {
                    Badges = new List<AccountBadgeModel>(),
                    Costs = new List<CostInfoModel>(),
                    Name = accountName
                });
                db.CostTypes.Add(new CostTypeModel
                {
                    Name = typeName,
                    CostInfoList = new EditableList<CostInfoModel>()
                });
                db.SaveChanges();
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "bread",
                    AccountId = 1,
                    TypeId = 1,
                    IsIncome = true,
                    Money = 25,
                    Created = DateTime.Now
                });
                db.SaveChanges();
            }

            // Act
            var item = _balanceFacade.GetItem(1);

            // Assert
            Assert.That(item != null, "Item was not got.");
        }
        /// <summary>
        /// Tests basic listing of CostInfo.
        /// </summary>
        [Test]
        public void ListItemTest1()
        {
            // Arrange
            const string accountName = "ExpenseManagerAccount01";
            const string typeName1 = "Food";
            const string typeName2 = "PC";
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(new AccountModel
                {
                    Badges = new List<AccountBadgeModel>(),
                    Costs = new List<CostInfoModel>(),
                    Name = accountName
                });
                db.CostTypes.Add(new CostTypeModel
                {
                    Name = typeName1,
                    CostInfoList = new EditableList<CostInfoModel>()
                });
                db.CostTypes.Add(new CostTypeModel
                {
                    Name = typeName2,
                    CostInfoList = new EditableList<CostInfoModel>()
                });
                db.SaveChanges();
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "bread",
                    AccountId = 1,
                    TypeId = 1,
                    IsIncome = true,
                    Money = 25,
                    Created = DateTime.Now
                });
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "WoW",
                    AccountId = 1,
                    TypeId = 2,
                    IsIncome = false,
                    Money = 1500,
                    Created = DateTime.Now
                });
                db.SaveChanges();
            }

            // Act
            var items = _balanceFacade.ListItems(new CostInfoFilter());

            // Assert
            Assert.That(items.Count == 2, "Items were not listed.");
        }

        /// <summary>
        /// Tests listing with filter.
        /// </summary>
        [Test]
        public void ListItemTest2()
        {
            // Arrange
            const string accountName = "ExpenseManagerAccount01";
            const string typeName1 = "Food";
            const string typeName2 = "PC";
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(new AccountModel
                {
                    Badges = new List<AccountBadgeModel>(),
                    Costs = new List<CostInfoModel>(),
                    Name = accountName
                });
                db.CostTypes.Add(new CostTypeModel
                {
                    Name = typeName1,
                    CostInfoList = new EditableList<CostInfoModel>()
                });
                db.CostTypes.Add(new CostTypeModel
                {
                    Name = typeName2,
                    CostInfoList = new EditableList<CostInfoModel>()
                });
                db.SaveChanges();
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "bread",
                    AccountId = 1,
                    TypeId = 1,
                    IsIncome = true,
                    Money = 25,
                    Created = DateTime.Now
                });
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "WoW",
                    AccountId = 1,
                    TypeId = 2,
                    IsIncome = false,
                    Money = 1500,
                    Created = DateTime.Now
                });
                db.SaveChanges();
            }

            // Act
            var items = _balanceFacade.ListItems(new CostInfoFilter { TypeId = 2 });

            // Assert
            Assert.That(items.Count == 1, "Item was not listed.");
        }
        /// <summary>
        /// Tests creation of Plan.
        /// </summary>
        [Test]
        public void CreatePlanTest()
        {
            // Arrange
            const string accountName = "ExpenseManagerAccount01";
            const string typeName = "Food";
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(new AccountModel
                {
                    Badges = new List<AccountBadgeModel>(),
                    Costs = new List<CostInfoModel>(),
                    Name = accountName
                });
                db.CostTypes.Add(new CostTypeModel
                {
                    Name = typeName,
                    CostInfoList = new EditableList<CostInfoModel>()
                });
                db.SaveChanges();
            }

            var plan = new Plan()
            {
                AccountId = 1,
                Description = "I want money for food!",
                PlanType = PlanType.Save,
                PlannedMoney = 10000,
                Deadline = DateTime.Today,
                IsCompleted = false,
                PlannedTypeId = 1
            };

            // Act
            _balanceFacade.CreatePlan(plan);

            // Assert
            var createdPlan = GetPlanById(1);
            Assert.That(createdPlan != null, "Plan was not created.");
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
        /// <summary>
        /// Tests creation of CostType.
        /// </summary>
        [Test]
        public void CreateItemTypeTest()
        {
            // Arrange
            const string typeName = "Food";

            var type = new CostType()
            {
                Name = typeName,
                CostInfoList = new EditableList<CostInfo>()
            };

            // Act
            _balanceFacade.CreateItemType(type);

            // Assert
            var createdType = GetTypeByName(typeName);
            Assert.That(createdType != null, "Type was not created.");
        }
        /// <summary>
        /// Tests deletion of CostType.
        /// </summary>
        [Test]
        public void DeleteItemTypeTest()
        {
            // Arrange
            const string typeName = "Food";
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.CostTypes.Add(new CostTypeModel
                {
                    Name = typeName,
                    CostInfoList = new EditableList<CostInfoModel>()
                });
                db.SaveChanges();
            }

            // Act
            _balanceFacade.DeleteItemType(1);

            // Assert
            var deletedType = GetTypeByName(typeName);
            Assert.That(deletedType == null, "Type was not deleted.");
        }
        /// <summary>
        /// Tests CostType update.
        /// </summary>
        [Test]
        public void UpdateItemTypeTest()
        {
            // Arrange
            const string typeName1 = "Food";
            const string typeName2 = "PC";
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.CostTypes.Add(new CostTypeModel
                {
                    Name = typeName1,
                    CostInfoList = new EditableList<CostInfoModel>()
                });
                db.SaveChanges();
            }

            // Act
            _balanceFacade.UpdateItemType(new CostType
            {
                Id = 1,
                Name = typeName2,
                CostInfoList = new EditableList<CostInfo>()
            });

            // Assert
            var updatedType = GetTypeByName(typeName2);
            Assert.That(updatedType != null, "Type was not updated.");
        }
        /// <summary>
        /// Tests CostType get.
        /// </summary>
        [Test]
        public void GetItemTypeTest()
        {
            // Arrange
            const string typeName = "Food";
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.CostTypes.Add(new CostTypeModel
                {
                    Name = typeName,
                    CostInfoList = new EditableList<CostInfoModel>()
                });
                db.SaveChanges();
            }

            // Act
            var type = _balanceFacade.GetItemType(1);

            // Assert
            Assert.That(type != null, "Type was not got.");
        }
        /// <summary>
        /// Test basic listing of CostInfos.
        /// </summary>
        [Test]
        public void ListItemTypesTest1()
        {
            // Arrange
            const string typeName1 = "Food";
            const string typeName2 = "PC";
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.CostTypes.Add(new CostTypeModel
                {
                    Name = typeName1,
                    CostInfoList = new EditableList<CostInfoModel>()
                });
                db.CostTypes.Add(new CostTypeModel
                {
                    Name = typeName2,
                    CostInfoList = new EditableList<CostInfoModel>()
                });
                db.SaveChanges();
            }

            // Act
            var types = _balanceFacade.ListItemTypes(new CostTypeFilter());

            // Assert
            Assert.That(types.Count == 2, "Types were not listed.");
        }
        /// <summary>
        /// Test listing with filter.
        /// </summary>
        [Test]
        public void ListItemTypesTest2()
        {
            // Arrange
            const string typeName1 = "Food";
            const string typeName2 = "PC";
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.CostTypes.Add(new CostTypeModel
                {
                    Name = typeName1,
                    CostInfoList = new EditableList<CostInfoModel>()
                });
                db.CostTypes.Add(new CostTypeModel
                {
                    Name = typeName2,
                    CostInfoList = new EditableList<CostInfoModel>()
                });
                db.SaveChanges();
            }

            // Act
            var types = _balanceFacade.ListItemTypes(new CostTypeFilter { Name = "PC" });

            // Assert
            Assert.That(types.Count == 1, "Type was not listed.");
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
            var badges = _balanceFacade.ListBadges(new BadgeFilter());

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
            var badges = _balanceFacade.ListBadges(new BadgeFilter { Name = badgeName1 });

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
        private static CostInfoModel GetItemById(int id)
        {
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                return db.CostInfos.Find(id);
            }
        }
        private static PlanModel GetPlanById(int id)
        {
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                return db.Plans.Find(id);
            }
        }
        private static CostTypeModel GetTypeByName(string typeName)
        {
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                return db.CostTypes.FirstOrDefault(model => model.Name.Equals(typeName)); ;
            }
        }

    }
}
