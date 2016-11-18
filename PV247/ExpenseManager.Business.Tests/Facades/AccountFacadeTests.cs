using System.Collections.Generic;
using System.Linq;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.Facades;
using ExpenseManager.Database;
using ExpenseManager.Database.Entities;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace ExpenseManager.Business.Tests.Facades
{
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
            const string accountName = "ExpenseManagerAccount01";
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

        [Test]
        public void UpdateAccount_ExistingAccount_UpdatesAccountName()
        {
            // Arrange
            const string accountName1 = "ExpenseManagerAccount01";
            const string accountName2 = "ExpenseManagerAccount02";
            var editedAccount = new Account
            {
                Badges = new List<AccountBadge>(),
                Costs = new List<CostInfo>(),
                Name = accountName2
            };
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                dbContext.Accounts.Add(new AccountModel
                {
                    Badges = new List<AccountBadgeModel>(),
                    Costs = new List<CostInfoModel>(),
                    Name = accountName1
                });
                dbContext.SaveChanges();
            }

            // Act
            _accountFacade.UpdateAccount(editedAccount);

            // Assert
            var updatedAccount = GetAccountByName(accountName2);
            Assert.That(updatedAccount != null, "Account name was not updated.");
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

        private static AccountModel GetAccountByName(string accountName)
        {
            using (var dbContext = new ExpenseDbContext(Effort.DbConnectionFactory.CreatePersistent(TestInstaller.ExpenseManagerTestDbConnection)))
            {
                return dbContext.Accounts.FirstOrDefault(accountEntity => accountEntity.Name.Equals(accountName));
            }
        }
    }
}
