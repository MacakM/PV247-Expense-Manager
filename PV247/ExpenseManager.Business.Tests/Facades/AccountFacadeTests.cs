using System;
using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.Facades;
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
        public void CreateAccount_NewAccount_CreatesAccount()
        {
            // Arrange
            var account = new Account
            {
                Badges = new List<AccountBadge>(),
                Costs = new List<CostInfo>(),
                Name = "ExpenseManagerAccount01"
            };

            // Act
            var createdAccountId = _accountFacade.CreateAccount(account);

            // Assert
            var createdAccount = GetAccountById(createdAccountId);
            Assert.That(createdAccount != null, "Account was not created.");
        }

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
            var editedAccount = new Account
            {
                Id = accountId,
                Badges = new List<AccountBadge>(),
                Costs = new List<CostInfo>(),
                Name = accountName2
            };

            // Act
            _accountFacade.UpdateAccount(editedAccount);

            // Assert
            var updatedAccount = GetAccountById(accountId);
            Assert.That(updatedAccount.Name.Equals(accountName2), "Account name was not updated.");
        }

        /*
        [Test]
        public void GetCurrentlySignedUserTest()
        {
            throw new AssertFailedException();
        }
        [Test]
        public void DeleteUserTest()
        {
            throw new AssertFailedException();
        }
        [Test]
        public void GetUserTest()
        {
            throw new AssertFailedException();
        }
        [Test]
        public void ListUsersTest()
        {
            throw new AssertFailedException();
        }
        [Test]
        public void CreateAccountTest()
        {
            throw new AssertFailedException();
        }
        [Test]
        public void DeleteAccountTest()
        {
            throw new AssertFailedException();
        }
        [Test]
        public void UpdateAccountTest()
        {
            throw new AssertFailedException();
        }
        [Test]
        public void GetAccountTest()
        {
            throw new AssertFailedException();
        }
        [Test]
        public void ListAccountsTest()
        {
            throw new AssertFailedException();
        }
        */

        private static AccountModel GetAccountById(Guid accountId)
        {
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                return dbContext.Accounts.Find(accountId);
            }
        }
    }
}
