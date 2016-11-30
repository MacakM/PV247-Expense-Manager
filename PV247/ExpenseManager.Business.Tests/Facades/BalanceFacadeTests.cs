using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.Facades;
using ExpenseManager.Database;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace ExpenseManager.Business.Tests.Facades
{
    /// <summary>
    /// Tests for balance facade
    /// </summary>
    [TestFixture]
    public class BalanceFacadeTests
    {
        private readonly BalanceFacade _balanceFacade = GlobalTestInitializer.Container.Resolve<BalanceFacade>();
        private readonly AccountFacade _accountFacade = GlobalTestInitializer.Container.Resolve<AccountFacade>();

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
        /// Test listing closeable plans
        /// </summary>
        [Test]
        public void ListAllCloseablePlans()
        {
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
                CostInfoList = new EditableList<CostInfoModel>()
            };
            var plan = new PlanModel
            {
                Description = "I want money for food!",
                PlanType = PlanTypeModel.Save,
                PlannedMoney = 10000,
                IsCompleted = false,
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
                plan.AccountId = accountId;
                plan.PlannedType = type;
                db.Plans.Add(plan);
                db.SaveChanges();
                var item = new CostInfoModel()
                {
                    Description = "bread",
                    AccountId = accountId,
                    TypeId = type.Id,
                    IsIncome = true,
                    Money = 10001,
                    Created = DateTime.Now.Subtract(new TimeSpan(100, 0, 0, 0)),
                    Account = db.Accounts.Find(accountId),
                    Type = db.CostTypes.Find(type.Id),
                    Periodicity = PeriodicityModel.None,
                    PeriodicMultiplicity = 3
                };
                db.CostInfos.Add(item);
                db.SaveChanges();
            }
            Assert.IsTrue(_balanceFacade.ListPlans(null, null).Count == 1, "Plan not present");
            Assert.IsTrue(_balanceFacade.ListAllCloseablePlans(account.Id).Count == 1, "Plan not found as closeable");
        }

        /// <summary>
        /// Tests closing plan by user
        /// </summary>
        [Test]
        public void ClosePlanTest()
        {
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
                CostInfoList = new EditableList<CostInfoModel>()
            };
            var plan = new PlanModel
            {
                Description = "I want money for food!",
                PlanType = PlanTypeModel.Save,
                PlannedMoney = 10000,
                IsCompleted = false,
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
                plan.AccountId = accountId;
                plan.PlannedType = type;
                db.Plans.Add(plan);
                db.SaveChanges();
                var item = new CostInfoModel()
                {
                    Description = "bread",
                    AccountId = accountId,
                    TypeId = type.Id,
                    IsIncome = true,
                    Money = 10001,
                    Created = DateTime.Now.Subtract(new TimeSpan(100, 0, 0, 0)),
                    Account = db.Accounts.Find(accountId),
                    Type = db.CostTypes.Find(type.Id),
                    Periodicity = PeriodicityModel.None,
                    PeriodicMultiplicity = 3
                };
                db.CostInfos.Add(item);
                db.SaveChanges();
            }
            var closeable = _balanceFacade.ListAllCloseablePlans(account.Id);
            var balance = _balanceFacade.GetBalance(account.Id);
            Assert.IsTrue(closeable.Count == 1, "Plan not found as closeable");
            _balanceFacade.ClosePlan(closeable.Single());
            Assert.IsTrue(balance == _balanceFacade.ListItems(null, null, null, null, null,null, null, false, null).Single().Money+ _balanceFacade.GetBalance(account.Id));
        }

        /// <summary>
        /// Check badges requirements test
        /// </summary>
        [Test]
        public void CheckBadgesRequirements()
        {
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
                CostInfoList = new EditableList<CostInfoModel>()
            };
            var plan = new PlanModel
            {
                Description = "I want money for food!",
                PlanType = PlanTypeModel.Save,
                PlannedMoney = 1000,
                PlannedType = type,
                IsCompleted = true,
                Start = DateTime.Now.Subtract(new TimeSpan(100, 0, 0, 0)),
                Deadline = DateTime.Now.Subtract(new TimeSpan(0, 0, 1, 0))
            };

            var plan1 = new PlanModel
            {
                Description = "I want money for food!",
                PlanType = PlanTypeModel.Save,
                PlannedMoney = 100000,
                PlannedType = type,
                IsCompleted = true,
                Start = DateTime.Now.Subtract(new TimeSpan(100, 0, 0, 0)),
                Deadline = DateTime.Now.Subtract(new TimeSpan(0, 0, 1, 0))
            };

            var plan2 = new PlanModel
            {
                Description = "I want money for food!",
                PlanType = PlanTypeModel.Save,
                PlannedMoney = 1001,
                PlannedType = type,
                IsCompleted = true,
                Start = DateTime.Now.Subtract(new TimeSpan(100, 0, 0, 0)),
                Deadline = DateTime.Now.Subtract(new TimeSpan(0, 0, 1, 0))
            };

            var plan3 = new PlanModel
            {
                Description = "I want money for food!",
                PlanType = PlanTypeModel.Save,
                PlannedMoney = 22221,
                PlannedType = type,
                IsCompleted = true,
                Start = DateTime.Now.Subtract(new TimeSpan(100, 0, 0, 0)),
                Deadline = DateTime.Now.Subtract(new TimeSpan(0, 0, 1, 0))
            };

            var plan4 = new PlanModel
            {
                Description = "I want money for food!",
                PlanType = PlanTypeModel.Save,
                PlannedMoney = 22221,
                PlannedType = type,
                IsCompleted = true,
                Start = DateTime.Now.Subtract(new TimeSpan(100, 0, 0, 0)),
                Deadline = DateTime.Now.Subtract(new TimeSpan(0, 0, 1, 0))
            };

            var plan5 = new PlanModel
            {
                Description = "I want money for food!",
                PlanType = PlanTypeModel.Save,
                PlannedMoney = 22221,
                PlannedType = type,
                IsCompleted = true,
                Start = DateTime.Now.Subtract(new TimeSpan(100, 0, 0, 0)),
                Deadline = DateTime.Now.Subtract(new TimeSpan(0, 0, 1, 0))
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
                plan.AccountId = accountId;
                plan.PlannedType = type;
                plan1.AccountId = accountId;
                plan1.PlannedType = type;
                plan2.AccountId = accountId;
                plan2.PlannedType = type;
                plan3.AccountId = accountId;
                plan3.PlannedType = type;
                plan4.AccountId = accountId;
                plan4.PlannedType = type;
                plan5.AccountId = accountId;
                plan5.PlannedType = type;

                db.Plans.Add(plan);
                db.Plans.Add(plan1);
                db.Plans.Add(plan2);
                db.Plans.Add(plan3);
                db.Plans.Add(plan4);
                db.Plans.Add(plan5);
                db.SaveChanges();
                var item = new CostInfoModel()
                {
                    Description = "bread",
                    AccountId = accountId,
                    TypeId = type.Id,
                    IsIncome = true,
                    Money = 10001,
                    Created = DateTime.Now.Subtract(new TimeSpan(100, 0, 0, 0)),
                    Account = db.Accounts.Find(accountId),
                    Type = db.CostTypes.Find(type.Id),
                    Periodicity = PeriodicityModel.None,
                    PeriodicMultiplicity = 3
                };
                db.CostInfos.Add(item);

                db.Badges.Add(new BadgeModel
                {
                    Accounts = new List<AccountBadgeModel>(),
                    BadgeImgUri = "badge.png",
                    Name = "PlanCompleter",
                    Description = "Complete at least 5 plans"
                });

                db.SaveChanges();
            }
            _balanceFacade.CheckBadgesRequirements();
            var badgedAccount = _accountFacade.GetAccount(account.Id);
            Assert.IsTrue(badgedAccount.Badges.Count>0);
        }

        /// <summary>
        /// Check max spend deadlines test
        /// </summary>
        [Test]
        public void CheckAllMaxSpendDeadlinesTest()
        {
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
                CostInfoList = new EditableList<CostInfoModel>()
            };
            var plan = new PlanModel
            {
                Description = "I want money for food!",
                PlanType = PlanTypeModel.MaxSpend,
                PlannedMoney = 10000,
                PlannedType = type,
                IsCompleted = false,
                Start = DateTime.Now.Subtract(new TimeSpan(100, 0, 0, 0)),
                Deadline = DateTime.Now.Add(new TimeSpan(0,0,1,0))
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
                plan.AccountId = accountId;
                plan.PlannedType = type;
                db.Plans.Add(plan);
                db.SaveChanges();
                var item = new CostInfoModel()
                {
                    Description = "bread",
                    AccountId = accountId,
                    TypeId = type.Id,
                    IsIncome = true,
                    Money = 999,
                    Created = DateTime.Now.Subtract(new TimeSpan(100, 0, 0, 0)),
                    Account = db.Accounts.Find(accountId),
                    Type = db.CostTypes.Find(type.Id),
                    Periodicity = PeriodicityModel.None,
                    PeriodicMultiplicity = 3
                };
                db.CostInfos.Add(item);
                db.SaveChanges();
            }
            _balanceFacade.CheckAllMaxSpendDeadlines();
            Assert.IsTrue(_balanceFacade.ListPlans(null, null).Count == 1);
            Assert.IsTrue(_balanceFacade.ListPlans(null, null).Single().IsCompleted);
        }

        /// <summary>
        /// Tests recomputing periodic costs into non periodic
        /// </summary>
        [Test]
        public void RecomputePeriodicCosts()
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
            var type = new CostTypeModel
            {
                Name = typeName,
                CostInfoList = new EditableList<CostInfoModel>()
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
                var item = new CostInfoModel()
                {
                    Description = "bread",
                    AccountId = accountId,
                    TypeId = typeId,
                    IsIncome = true,
                    Money = 25,
                    Created = DateTime.Now.Subtract(new TimeSpan(100,0,0,0)),
                    Account = db.Accounts.Find(accountId),
                    Type = db.CostTypes.Find(typeId),
                    Periodicity = PeriodicityModel.Month,
                    PeriodicMultiplicity = 3
                };
                db.CostInfos.Add(item);
                db.SaveChanges();
            }
          
           
            _balanceFacade.RecomputePeriodicCosts();
            var result = _balanceFacade.ListItems(null, Periodicity.None, null);
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.Single().Description.Equals("bread"));


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
                CostInfoList = new EditableList<CostInfoModel>()
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
            var createdId = _balanceFacade.CreateItem(item);

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
                CostInfoList = new EditableList<CostInfoModel>()
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
            _balanceFacade.DeleteItem(infoId);

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
                CostInfoList = new EditableList<CostInfoModel>()
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
            _balanceFacade.UpdateItem(new CostInfo
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
                CostInfoList = new EditableList<CostInfoModel>()
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
            var item = _balanceFacade.GetItem(infoId);

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
                CostInfoList = new EditableList<CostInfoModel>()
            };
            var type2 = new CostTypeModel
            {
                Name = typeName2,
                CostInfoList = new EditableList<CostInfoModel>()
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
            var items = _balanceFacade.ListItems(null, null, null);

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
                CostInfoList = new EditableList<CostInfoModel>()
            };
            var type2 = new CostTypeModel
            {
                Name = typeName2,
                CostInfoList = new EditableList<CostInfoModel>()
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
            var items = _balanceFacade.ListItems(typeId2, null);

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
                CostInfoList = new EditableList<CostInfoModel>()
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

            var plan = new Plan()
            {
                AccountId = accountId,
                Description = "I want money for food!",
                PlanType = PlanType.Save,
                PlannedMoney = 10000,
                Deadline = DateTime.Today,
                IsCompleted = false,
                PlannedTypeId = typeId
            };

            // Act
            var planId = _balanceFacade.CreatePlan(plan);

            // Assert
            var createdPlan = GetPlanById(planId);
            Assert.That(createdPlan != null, "Plan was not created.");
        }
        /// <summary>
        /// Tests plan deletion.
        /// </summary>
        [Test]
        public void DeletePlanTest()
        {
            // Arrange
            Guid planId;
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
                CostInfoList = new EditableList<CostInfoModel>()
            };
            var plan = new PlanModel
            {
                Description = "I want money for food!",
                PlanType = PlanTypeModel.Save,
                PlannedMoney = 10000,
                Deadline = DateTime.Today,
                IsCompleted = false,
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
                plan.AccountId = accountId;
                plan.PlannedType = type;
                db.Plans.Add(plan);
                db.SaveChanges();
                planId = plan.Id;
            }

            // Act
            _balanceFacade.DeletePlan(planId);

            // Assert
            var deletedPlan = GetPlanById(planId);
            Assert.That(deletedPlan == null, "Plan was not deleted.");
        }
        /// <summary>
        /// Tests Plan update.
        /// </summary>
        [Test]
        public void UpdatePlanTest()
        {
            // Arrange
            Guid accountId;
            Guid planId;
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
                CostInfoList = new EditableList<CostInfoModel>()
            };
            var plan = new PlanModel
            {
                Description = "I want money for food!",
                PlanType = PlanTypeModel.Save,
                PlannedMoney = 10000,
                Deadline = DateTime.Today.AddDays(5),
                Start = DateTime.Today,
                IsCompleted = false,
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
                typeId = plan.Id;
                plan.AccountId = accountId;
                plan.PlannedType = type;
                db.Plans.Add(plan);
                db.SaveChanges();
                planId = plan.Id;
            }

            // Act
            _balanceFacade.UpdatePlan(new Plan
            {
                Id = planId,
                Description = "I want money for games!",
                PlanType = PlanType.Save,
                PlannedMoney = 10000,
                Deadline = DateTime.Today,
                IsCompleted = false,
                AccountId = accountId,
                PlannedTypeId = typeId
            });

            // Assert
            var updatedPlan = GetPlanById(planId);
            Assert.That(updatedPlan.Description.Equals("I want money for games!"), "Plan was not updated.");
        }
        /// <summary>
        /// Test Plan get.
        /// </summary>
        [Test]
        public void GetPlanTest()
        {
            // Arrange
            Guid planId;
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
                CostInfoList = new EditableList<CostInfoModel>()
            };
            var plan = new PlanModel
            {
                Description = "I want money for food!",
                PlanType = PlanTypeModel.Save,
                PlannedMoney = 10000,
                Deadline = DateTime.Today,
                IsCompleted = false,
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
                plan.AccountId = accountId;
                plan.PlannedType = type;
                db.Plans.Add(plan);
                db.SaveChanges();
                planId = plan.Id;
            }

            // Act
            var myPlan = _balanceFacade.GetPlan(planId);

            // Assert
            Assert.That(myPlan != null, "Plan was not got.");
        }
        /// <summary>
        /// Test basic listing of Plans.
        /// </summary>
        [Test]
        public void ListPlansTest1()
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
            var type = new CostTypeModel
            {
                Name = typeName,
                CostInfoList = new EditableList<CostInfoModel>()
            };
            var plan = new PlanModel
            {
                Description = "I want money for food!",
                PlanType = PlanTypeModel.Save,
                PlannedMoney = 10000,
                Deadline = DateTime.Today,
                IsCompleted = false,
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
                plan.AccountId = accountId;
                plan.PlannedType = type;
                db.Plans.Add(plan);
                db.SaveChanges();
            }
            Assert.IsTrue(_balanceFacade.ListPlans(null, null).Count > 0);
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
            Guid typeId;
            const string typeName = "Food";
            var type = new CostTypeModel
            {
                Name = typeName,
                CostInfoList = new EditableList<CostInfoModel>()
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.CostTypes.Add(type);
                db.SaveChanges();
                typeId = type.Id;
            }

            // Act
            _balanceFacade.DeleteItemType(typeId);

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
            Guid typeId;
            var type = new CostTypeModel
            {
                Name = typeName1,
                CostInfoList = new EditableList<CostInfoModel>()
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.CostTypes.Add(type);
                db.SaveChanges();
                typeId = type.Id;
            }

            // Act
            _balanceFacade.UpdateItemType(new CostType
            {
                Id = typeId,
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
            Guid typeId;
            var type = new CostTypeModel
            {
                Name = typeName,
                CostInfoList = new EditableList<CostInfoModel>()
            };
            using (
                var db =
                    new ExpenseDbContext(
                        Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                db.CostTypes.Add(type);
                db.SaveChanges();
                typeId = type.Id;
            }

            // Act
            var myType = _balanceFacade.GetItemType(typeId);

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
            const string typeName1 = "Food";
            const string typeName2 = "PC";
            var type1 = new CostTypeModel
            {
                Name = typeName1,
                CostInfoList = new EditableList<CostInfoModel>()
            };
            var type2 = new CostTypeModel
            {
                Name = typeName2,
                CostInfoList = new EditableList<CostInfoModel>()
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
            var types = _balanceFacade.ListItemTypes(null, null);

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
            var type1 = new CostTypeModel
            {
                Name = typeName1,
                CostInfoList = new EditableList<CostInfoModel>()
            };
            var type2 = new CostTypeModel
            {
                Name = typeName2,
                CostInfoList = new EditableList<CostInfoModel>()
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
            var types = _balanceFacade.ListItemTypes("PC", null);

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
            Guid badgeId;
            var badge = new BadgeModel()
            {
                Name = badgeName,
                Description = "Add your first expense",
                BadgeImgUri = "picture"
            };
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Badges.Add(badge);
                dbContext.SaveChanges();
                badgeId = badge.Id;
            }

            // Act
            _balanceFacade.DeleteBadge(badgeId);

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
            var badge = new BadgeModel()
            {
                Name = badgeName,
                Description = "Add your first expense",
                BadgeImgUri = "picture"
            };
            Guid badgeId;
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Badges.Add(badge);
                dbContext.SaveChanges();
                badgeId = badge.Id;
            }

            // Act
            var myBadge = _balanceFacade.GetBadge(badgeId);

            // Assert
            Assert.That(myBadge.Name == badgeName, "Badge was not got.");
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
            var badges = _balanceFacade.ListBadges(null, null);

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
            var badges = _balanceFacade.ListBadges(badgeName1, null);

            // Assert
            Assert.That(badges.Count == 1, "Badge was not listed.");
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

        private static PlanModel GetPlanById(Guid id)
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
                return db.CostTypes.FirstOrDefault(model => model.Name.Equals(typeName)); 
            }
        }

    }
}
