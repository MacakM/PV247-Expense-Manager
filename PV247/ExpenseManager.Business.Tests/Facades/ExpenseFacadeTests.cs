using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.Facades;
using ExpenseManager.Business.Tests.Bootstrap;
using ExpenseManager.Database;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;
using NUnit.Framework;

namespace ExpenseManager.Business.Tests.Facades
{
    /// <summary>
    /// Tests for balance facade
    /// </summary>
    [TestFixture]
    public class ExpenseFacadeTests
    {
        private readonly ExpenseFacade _expenseFacade = GlobalTestInitializer.Container.Resolve<ExpenseFacade>();

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
        
        /// <summary>
        /// Tests CostInfo creation.
        /// </summary>
        [Test]
        public void CreateItemTest()
        {
            // Arrange
            Guid accountId;
            Guid typeId;
            const string accountName = "ExpenseManagerAccount01";
            const string typeName = "Food";
            var account = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = accountName
            };
            var type = new CostTypeModel
            {
                Name = typeName,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(account);
                db.CostTypes.Add(type);
                db.SaveChanges();
                accountId = account.Id;
                typeId = type.Id;
            }

            var item = new CostInfo()
            {
                Description = "bread",
                AccountId = accountId,
                TypeId = typeId,
                IsIncome = true,
                Money = 25,
                Created = DateTime.Now
            };

            // Act
            var createdId = _expenseFacade.CreateItem(item);

            // Assert
            var createdItem = GetItemById(createdId);
            Assert.That(createdItem != null, "Item was not created.");
        }
        /// <summary>
        /// Test CostInfo deletion.
        /// </summary>
        [Test]
        public void DeleteItemTest()
        {
            // Arrange
            Guid infoId;
            const string accountName = "ExpenseManagerAccount01";
            const string typeName = "Food";
            var account = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = accountName
            };
            var type = new CostTypeModel
            {
                Name = typeName,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            var info = new CostInfoModel
            {
                Description = "bread",
                IsIncome = true,
                Money = 25,
                Created = DateTime.Now
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(account);
                db.CostTypes.Add(type);
                db.SaveChanges();
                var accountId = account.Id;
                var typeId = type.Id;
                info.AccountId = accountId;
                info.TypeId = typeId;
                db.CostInfos.Add(info);
                db.SaveChanges();
                infoId = info.Id;
            }

            // Act
            _expenseFacade.DeleteItem(infoId);

            // Assert
            var deletedItem = GetItemById(infoId);
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
            Guid accountId;
            Guid typeId;
            Guid infoId;
            var account = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = accountName
            };
            var type = new CostTypeModel
            {
                Name = typeName,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            var info = new CostInfoModel
            {
                Description = "bread",
                IsIncome = true,
                Money = 25,
                Created = DateTime.Now
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(account);
                db.CostTypes.Add(type);
                db.SaveChanges();
                accountId = account.Id;
                typeId = type.Id;
                info.AccountId = accountId;
                info.TypeId = typeId;
                db.CostInfos.Add(info);
                db.SaveChanges();
                infoId = info.Id;
            }

            // Act
            _expenseFacade.UpdateItem(new CostInfo
            {
                Id = infoId,
                Description = "bread",
                AccountId = accountId,
                TypeId = typeId,
                IsIncome = true,
                Money = 50,
                Created = DateTime.Now
            });

            // Assert
            Assert.That(GetItemById(infoId).Money == 50, "Item was not updated.");
        }
        /// <summary>
        /// Tests get of CostInfo.
        /// </summary>
        [Test]
        public void GetItemTest()
        {
            // Arrange
            Guid infoId;
            const string accountName = "ExpenseManagerAccount01";
            const string typeName = "Food";
            var account = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = accountName
            };
            var type = new CostTypeModel
            {
                Name = typeName,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            var info = new CostInfoModel
            {
                Description = "bread",
                IsIncome = true,
                Money = 25,
                Created = DateTime.Now
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(account);
                db.CostTypes.Add(type);
                db.SaveChanges();
                var accountId = account.Id;
                var typeId = type.Id;
                info.AccountId = accountId;
                info.TypeId = typeId;
                db.CostInfos.Add(info);
                db.SaveChanges();
                infoId = info.Id;
            }

            // Act
            var item = _expenseFacade.GetItem(infoId);

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
            var account = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = accountName
            };
            var type1 = new CostTypeModel
            {
                Name = typeName1,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            var type2 = new CostTypeModel
            {
                Name = typeName2,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(account);
                db.CostTypes.Add(type1);
                db.CostTypes.Add(type2);
                db.SaveChanges();
                var accountId = account.Id;
                var typeId1 = type1.Id;
                var typeId2 = type2.Id;
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "bread",
                    AccountId = accountId,
                    TypeId = typeId1,
                    IsIncome = true,
                    Money = 25,
                    Created = DateTime.Now
                });
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "WoW",
                    AccountId = accountId,
                    TypeId = typeId2,
                    IsIncome = false,
                    Money = 1500,
                    Created = DateTime.Now
                });
                db.SaveChanges();
            }

            // Act
            var items = _expenseFacade.ListItems(null, null, null);

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
            Guid typeId2;
            var account = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = accountName
            };
            var type1 = new CostTypeModel
            {
                Name = typeName1,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            var type2 = new CostTypeModel
            {
                Name = typeName2,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(account);
                db.CostTypes.Add(type1);
                db.CostTypes.Add(type2);
                db.SaveChanges();
                var accountId = account.Id;
                var typeId1 = type1.Id;
                typeId2 = type2.Id;
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "bread",
                    AccountId = accountId,
                    TypeId = typeId1,
                    IsIncome = true,
                    Money = 25,
                    Created = DateTime.Now
                });
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "WoW",
                    AccountId = accountId,
                    TypeId = typeId2,
                    IsIncome = false,
                    Money = 1500,
                    Created = DateTime.Now
                });
                db.SaveChanges();
            }

            // Act
            var items = _expenseFacade.ListItems(typeId2, null);

            // Assert
            Assert.That(items.Count == 1, "Item was not listed.");
        }

        /// <summary>
        /// Tests getting number of incomes.
        /// </summary>
        [Test]
        public void GetItemCountTest1()
        {
            // Arrange
            const string accountName = "ExpenseManagerAccount01";
            const string typeName1 = "Food";
            const string typeName2 = "PC";
            var account = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = accountName
            };
            var type1 = new CostTypeModel
            {
                Name = typeName1,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            var type2 = new CostTypeModel
            {
                Name = typeName2,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(account);
                db.CostTypes.Add(type1);
                db.CostTypes.Add(type2);
                db.SaveChanges();
                var accountId = account.Id;
                var typeId1 = type1.Id;
                var typeId2 = type2.Id;
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "bread",
                    AccountId = accountId,
                    TypeId = typeId1,
                    IsIncome = true,
                    Money = 25,
                    Created = DateTime.Now
                });
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "WoW",
                    AccountId = accountId,
                    TypeId = typeId2,
                    IsIncome = false,
                    Money = 1500,
                    Created = DateTime.Now
                });
                db.SaveChanges();
            }

            // Act
            var incomeCount = _expenseFacade.GetCostInfosCount(null, null, null, null, null,null, null, true);

            // Assert
            Assert.That(incomeCount == 1, "Number of incomes is not correct.");
        }

        /// <summary>
        /// Tests getting number of cost information for given account.
        /// </summary>
        [Test]
        public void GetItemCountTest2()
        {
            // Arrange
            Guid accountId;
            const string accountName = "ExpenseManagerAccount01";
            const string typeName1 = "Food";
            const string typeName2 = "PC";
            var account = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = accountName
            };
            var type1 = new CostTypeModel
            {
                Name = typeName1,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            var type2 = new CostTypeModel
            {
                Name = typeName2,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(account);
                db.CostTypes.Add(type1);
                db.CostTypes.Add(type2);
                db.SaveChanges();
                accountId = account.Id;
                var typeId1 = type1.Id;
                var typeId2 = type2.Id;
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "bread",
                    AccountId = accountId,
                    TypeId = typeId1,
                    IsIncome = true,
                    Money = 25,
                    Created = DateTime.Now
                });
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "WoW",
                    AccountId = accountId,
                    TypeId = typeId2,
                    IsIncome = false,
                    Money = 1500,
                    Created = DateTime.Now
                });
                db.SaveChanges();
            }

            // Act
            var accountItemsCount = _expenseFacade.GetCostInfosCount(accountId, null, null, null, null, null, null, null);

            // Assert
            Assert.That(accountItemsCount == 2, "Number of items for given account is not correct.");
        }

        /// <summary>
        /// Tests getting number of cost information beetween money range.
        /// </summary>
        [Test]
        public void GetItemCountTest3()
        {
            // Arrange
            const string accountName = "ExpenseManagerAccount01";
            const string typeName1 = "Food";
            const string typeName2 = "PC";
            var account = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = accountName
            };
            var type1 = new CostTypeModel
            {
                Name = typeName1,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            var type2 = new CostTypeModel
            {
                Name = typeName2,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(account);
                db.CostTypes.Add(type1);
                db.CostTypes.Add(type2);
                db.SaveChanges();
                var accountId = account.Id;
                var typeId1 = type1.Id;
                var typeId2 = type2.Id;
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "bread",
                    AccountId = accountId,
                    TypeId = typeId1,
                    IsIncome = true,
                    Money = 25,
                    Created = DateTime.Now
                });
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "WoW",
                    AccountId = accountId,
                    TypeId = typeId2,
                    IsIncome = false,
                    Money = 1500,
                    Created = DateTime.Now
                });
                db.SaveChanges();
            }

            // Act
            var cheapItemsCount = _expenseFacade.GetCostInfosCount(null, null, null, null, 25, 200, null, null);

            // Assert
            Assert.That(cheapItemsCount == 1, "Number of cheap items is not correct.");
        }

        /// <summary>
        /// Tests getting number of cost information with many constraints.
        /// </summary>
        [Test]
        public void GetItemCountTest4()
        {
            // Arrange
            Guid accountId;
            Guid typeId1;
            const string accountName = "ExpenseManagerAccount01";
            const string typeName1 = "Food";
            const string typeName2 = "PC";
            var account = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = accountName
            };
            var type1 = new CostTypeModel
            {
                Name = typeName1,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            var type2 = new CostTypeModel
            {
                Name = typeName2,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(account);
                db.CostTypes.Add(type1);
                db.CostTypes.Add(type2);
                db.SaveChanges();
                accountId = account.Id;
                typeId1 = type1.Id;
                var typeId2 = type2.Id;
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "bread",
                    AccountId = accountId,
                    TypeId = typeId1,
                    IsIncome = true,
                    Money = 25,
                    Created = DateTime.Now
                });
                db.CostInfos.Add(new CostInfoModel
                {
                    Description = "WoW",
                    AccountId = accountId,
                    TypeId = typeId2,
                    IsIncome = false,
                    Money = 1500,
                    Created = DateTime.Now
                });
                db.SaveChanges();
            }

            // Act
            var mixedItemsCount = _expenseFacade.GetCostInfosCount(accountId, null, null, null, 25, 2000, typeId1, true);

            // Assert
            Assert.That(mixedItemsCount == 1, "Number of mixed items is not correct.");
        }

        /// <summary>
        /// Tests creation of CostType.
        /// </summary>
        [Test]
        public void CreateItemTypeTest()
        {
            // Arrange
            const string accountName = "ExpenseManagerAccount01";
            const string typeName = "Food";
            var account = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = accountName
            };

            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(account);
                db.SaveChanges();
            }

            var type = new CostType()
            {
                Name = typeName,
                CostInfoList = new EditableList<CostInfo>(),
                AccountId = account.Id
            };

            // Act
            _expenseFacade.CreateItemType(type);

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
            Guid typeId;
            const string accountName = "ExpenseManagerAccount01";
            const string typeName = "Food";
            var account = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = accountName
            };

            var type = new CostTypeModel
            {
                Name = typeName,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };

            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(account);
                db.CostTypes.Add(type);
                db.SaveChanges();
                typeId = type.Id;
            }

            // Act
            _expenseFacade.DeleteItemType(typeId);

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
            const string accountName = "ExpenseManagerAccount01";
            var account = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = accountName
            };
            const string typeName1 = "Food";
            const string typeName2 = "PC";
            Guid typeId;
            var type = new CostTypeModel
            {
                Name = typeName1,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(account);
                db.CostTypes.Add(type);
                db.SaveChanges();
                typeId = type.Id;
            }

            // Act
            _expenseFacade.UpdateItemType(new CostType
            {
                Id = typeId,
                Name = typeName2,
                CostInfoList = new EditableList<CostInfo>(),
                AccountId = account.Id
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
            const string accountName = "ExpenseManagerAccount01";
            var account = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = accountName
            };

            const string typeName = "Food";
            Guid typeId;
            var type = new CostTypeModel
            {
                Name = typeName,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.Accounts.Add(account);
                db.CostTypes.Add(type);
                db.SaveChanges();
                typeId = type.Id;
            }

            // Act
            var myType = _expenseFacade.GetItemType(typeId);

            // Assert
            Assert.That(myType != null, "Type was not got.");
        }

        /// <summary>
        /// Test basic listing of CostInfos.
        /// </summary>
        [Test]
        public void ListItemTypesTest1()
        {
            // Arrange
            const string accountName = "ExpenseManagerAccount01";
            var account = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = accountName
            };

            const string typeName1 = "Food";
            const string typeName2 = "PC";
            var type1 = new CostTypeModel
            {
                Name = typeName1,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            var type2 = new CostTypeModel
            {
                Name = typeName2,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.CostTypes.Add(type1);
                db.CostTypes.Add(type2);
                db.SaveChanges();
            }

            // Act
            var types = _expenseFacade.ListItemTypes(null, account.Id, null);

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
            const string accountName = "ExpenseManagerAccount01";
            var account = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = accountName
            };

            const string typeName1 = "Food";
            const string typeName2 = "PC";
            var type1 = new CostTypeModel
            {
                Name = typeName1,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            var type2 = new CostTypeModel
            {
                Name = typeName2,
                CostInfoList = new EditableList<CostInfoModel>(),
                Account = account
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.CostTypes.Add(type1);
                db.CostTypes.Add(type2);
                db.SaveChanges();
            }

            // Act
            var types = _expenseFacade.ListItemTypes("PC", account.Id, null);

            // Assert
            Assert.That(types.Count == 1, "Type was not listed.");
        }

        private static CostInfoModel GetItemById(Guid id)
        {
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                return db.CostInfos.Find(id);
            }
        }
        
        private static CostTypeModel GetTypeByName(string typeName)
        {
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                return db.CostTypes.FirstOrDefault(model => model.Name.Equals(typeName));
            }
        }

    }
}
