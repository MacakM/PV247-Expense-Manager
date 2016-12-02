using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.Facades;
using ExpenseManager.Business.Tests.Bootstrap;
using ExpenseManager.Database;
using ExpenseManager.Database.Entities;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace ExpenseManager.Business.Tests.Facades
{
    /// <summary>
    /// Integration tests for account facade.
    /// </summary>
    [TestFixture]
    public class AccountFacadeTests
    {
        private readonly AccountFacade _accountFacade = GlobalTestInitializer.Container.Resolve<AccountFacade>();
        private readonly IRuntimeMapper _mapper = GlobalTestInitializer.Container.Resolve<Mapper>().DefaultContext.Mapper;

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
        /// Test creating new account
        /// </summary>
        [Test]
        public void CreateAccount_NewAccount_CreatesAccount()
        {
            const string accountName = "ExpenseManagerAccount01";

            // Arrange
            var account = new Account
            {
                Badges = new List<AccountBadge>(),
                Costs = new List<CostInfo>(),
                Name = accountName
            };

            // Act
            _accountFacade.CreateAccount(account);

            // Assert
            var createdAccount = GetAccountByName(accountName);
            Assert.That(createdAccount != null, "Account was not created.");
        }

        /// <summary>
        /// Tests updating existing account
        /// </summary>
        [Test]
        public void UpdateAccount_ExistingAccount_UpdatesAccountName()
        {
            // Arrange
            const string accountName2 = "ExpenseManagerAccount02";
            Guid accountId;      
            var accountEntity = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = "ExpenseManagerAccount01"
            };
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Accounts.Add(accountEntity);
                dbContext.SaveChanges();
                accountId = accountEntity.Id;
            }
            var editedAccount = _mapper.Map<AccountModel, Account>(accountEntity);
            editedAccount.Name = accountName2;

            // Act
            _accountFacade.UpdateAccount(editedAccount);

            // Assert
            var updatedAccount = GetAccountById(accountId);
            Assert.That(updatedAccount.Name.Equals(accountName2), "Account name was not updated.");
        }
      
        /// <summary>
        /// Tests getting currently signer existing user
        /// </summary>
        [Test]
        public void GetCurrentlySignedUser_ExistingUser_ReturnCorrectUser()
        {
            Guid userId;
            var user = new User
            {
                AccessType = AccountAccessType.Full,
                Email = "demo@mail.com",
                Name = "SomeUser"
            };
            var userModel = _mapper.Map<User, UserModel>(user);
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Users.Add(userModel);
                dbContext.SaveChanges();
                userId = userModel.Id;
            }
            user.Id = userId;

            // Act
            var currentlySignedUser = _accountFacade.GetCurrentlySignedUser(user.Email);

            // Assert
            Assert.AreEqual(currentlySignedUser, user, "GetCurrentlySignedUser failed - users do not match.");
        }
 
        /// <summary>
        /// Test deleting an existing user
        /// </summary>
        [Test]
        public void DeleteUser_ExistingUser_UserIsNotPresentInTheDB()
        {
            Guid userId;
            var user = new User
            {
                AccessType = AccountAccessType.Full,
                Email = "demo@mail.com",
                Name = "SomeUser"
            };
            var userModel = _mapper.Map<User, UserModel>(user);
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Users.Add(userModel);
                dbContext.SaveChanges();
                userId = userModel.Id;
            }
            user.Id = userId;

            // Act
            _accountFacade.DeleteUser(user.Id);
            
            // Assert
            bool userExistsInDb;           
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                userExistsInDb = dbContext.Users.Find(user.Id) != null;
            }
            
            Assert.AreEqual(userExistsInDb, false, "DeleteUser failed - users still exists in the db.");
        }
        
        /// <summary>
        /// Tests getting existing user
        /// </summary>
        [Test]
        public void GetUser_ExistingUser_ReturnCorrectUser()
        {
            Guid userId;
            var user = new User
            {
                AccessType = AccountAccessType.Full,
                Email = "demo@mail.com",
                Name = "SomeUser"
            };
            var userModel = _mapper.Map<User, UserModel>(user);
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Users.Add(userModel);
                dbContext.SaveChanges();
                userId = userModel.Id;
            }
            user.Id = userId;

            // Act
            var obtainedUser = _accountFacade.GetUser(user.Id);

            // Assert
            Assert.AreEqual(obtainedUser, user, "GetUser failed - users do not match.");
        }

        /// <summary>
        /// Tests listing some existing users
        /// </summary>
        [Test]
        public void ListUsers_CoupleOfExistingUsers_FiltersUser()
        {
            var user1 = new User
            {
                AccessType = AccountAccessType.Full,
                Email = "demo@mail.com",
                Name = "SomeUser"
            };
            var userModel1 = _mapper.Map<User, UserModel>(user1);
            var user2 = new User
            {
                AccessType = AccountAccessType.Read,
                Email = "demo2@mail.com",
                Name = "SomeUser2"
            };
            var userModel2 = _mapper.Map<User, UserModel>(user2);
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Users.Add(userModel1);
                dbContext.Users.Add(userModel2);
                dbContext.SaveChanges();
            }

            // Act
            var obtainedUsers = _accountFacade.ListUsers(null, AccountAccessType.Read, null , null);

            // Assert
            Assert.That(obtainedUsers.Count == 1 && obtainedUsers.First().AccessType == AccountAccessType.Read, "ListUsers failed - actual result does not match the expected one.");
        }

        /// <summary>
        /// Tests deleting an existing account 
        /// </summary>
        [Test]
        public void DeleteAccount_ExistingAccount_AccountIsNotPresentInTheDB()
        {
            // Arrange
            var accountModel = new AccountModel
            {
                Badges = new List<AccountBadgeModel>(),
                Costs = new List<CostInfoModel>(),
                Name = "ExpenseManagerAccount01"
            };

            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Accounts.Add(accountModel);
                dbContext.SaveChanges();
            }
            var accountId = accountModel.Id;

            // Act
            _accountFacade.DeleteAccount(accountId);

            // Assert
            bool accountExistsInDb;
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                accountExistsInDb = dbContext.Users.Find(accountId) != null;
            }

            Assert.That(!accountExistsInDb, "Account was not removed.");
        }
    
        /// <summary>
        /// Test GetAccount method getting an existing account
        /// </summary>
        [Test]
        public void GetAccount_ExistingAccount_ReturnCorrectAccount()
        {
            var account = new Account
            {
                Badges = new List<AccountBadge>(),
                Costs = new List<CostInfo>(),
                Name = "ExpenseManagerAccount01"
            };
            var accountModel = _mapper.Map<Account, AccountModel>(account);
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Accounts.Add(accountModel);
                dbContext.SaveChanges();               
            }
            var accountId = accountModel.Id;

            // Act
            var obtainedAccount = _accountFacade.GetAccount(accountId);

            // Assert
            Assert.AreEqual(obtainedAccount, account, "GetAccount failed - accounts do not match.");
        }

        /// <summary>
        /// Tests list accounts method getting some existing ones
        /// </summary>
        [Test]
        public void ListAccounts_CoupleOfExistingAccounts_FiltersAccount()
        {
            const string account2Name = "ExpenseManagerAccount02";
            var account1 = new Account
            {
                Badges = new List<AccountBadge>(),
                Costs = new List<CostInfo>(),
                Name = "ExpenseManagerAccount01"
            };
            var accountModel1 = _mapper.Map<Account, AccountModel>(account1);
            var account2 = new Account
            {
                Badges = new List<AccountBadge>(),
                Costs = new List<CostInfo>(),
                Name = account2Name
            };
            var accountModel2 = _mapper.Map<Account, AccountModel>(account2);
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Accounts.Add(accountModel1);
                dbContext.Accounts.Add(accountModel2);
                dbContext.SaveChanges();
            }

            // Act
            var obtainedAccounts = _accountFacade.ListAccounts(account2Name,null);

            // Assert
            Assert.That(obtainedAccounts.Count == 1 && obtainedAccounts.First().Name.Equals(account2Name), "ListAccounts failed - actual result does not match with expected one");
        }
        
        private static AccountModel GetAccountByName(string accountName)
        {
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                return dbContext.Accounts.FirstOrDefault(account => account.Name.Equals(accountName));
            }
        }

        private static AccountModel GetAccountById(Guid accountId)
        {
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                return dbContext.Accounts.Find(accountId);
            }
        }
    }
}
