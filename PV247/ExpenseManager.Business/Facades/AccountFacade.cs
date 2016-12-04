using System;
using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.DataTransferObjects.Factories;
using ExpenseManager.Business.Infrastructure.CastleWindsor;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.Facades
{
    /// <summary>
    /// Provides access to user related functionality
    /// </summary>
    public class AccountFacade
    {
        private readonly IUserService _userService = BusinessLayerDIManager.Resolve<IUserService>();

        private readonly IAccountService _accountService = BusinessLayerDIManager.Resolve<IAccountService>();

        #region User CRUD
        /// <summary>
        /// Registers user according to provided information
        /// </summary>
        /// <param name="userRegistration">User registration information</param>
        /// <param name="createAccount">If account should be created</param>
        public void RegisterNewUser(User userRegistration, bool createAccount = true)
        {
            _userService.RegisterNewUser(userRegistration);
            if (createAccount)
            {
                CreateAccount(userRegistration.Id);
            }
        }

        /// <summary>
        /// Updates existing user according to provided information
        /// </summary>
        /// <param name="modifiedUser">Updated user information</param>
        public void UpdateUser(User modifiedUser)
        {
            _userService.UpdateUser(modifiedUser);
        }

        /// <summary>
        /// Gets currently signed user according to its email
        /// </summary>
        /// <param name="email">User unique email</param>
        /// <param name="includeAllProperties">Decides whether all properties should be included</param>
        /// <returns>User with user details</returns>
        public User GetCurrentlySignedUser(string email, bool includeAllProperties = false)
        {
            return _userService.GetCurrentlySignedUser(email, includeAllProperties);
        }

        /// <summary>
        /// Delete user specified by userId
        /// </summary>
        /// <param name="userId">Unique user identifier</param>
        public void DeleteUser(Guid userId)
        {
            _userService.DeleteUser(userId);
        }

        /// <summary>
        /// Get specific user that had id == userId
        /// </summary>
        /// <param name="userId">Unique user identifier</param>
        /// <returns>One user with id == userId</returns>
        public User GetUser(Guid userId)
        {
            return _userService.GetUser(userId);
        }

        /// <summary>
        /// List users that match parameters given in filter 
        /// </summary>
        /// <param name="accessType"></param>
        /// <param name="email"></param>
        /// <param name="accountId"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<User> ListUsers(Guid? accountId, AccountAccessType? accessType, string email, PageInfo pageInfo)
        {
            var filters = FilterFactory.GetUserFilters(accountId,accessType, email);
            return _userService.ListUsers(filters, FilterFactory.GetPageAndOrderable<UserModel>(pageInfo));
        }

        #endregion
        #region Account CRUD
        /// <summary>
        /// Creates new account
        /// </summary>
        /// <param name="account"></param>
        public void CreateAccount(Account account)
        {
            _accountService.CreateAccount(account);
        }

        /// <summary>
        /// Creates account for user with given id
        /// </summary>
        public Guid CreateAccount(Guid userId)
        {
            return _accountService.CreateAccount(userId);
        }

        /// <summary>
        /// Deletes account by specified unique id
        /// </summary>
        /// <param name="accountId"></param>
        public void DeleteAccount(Guid accountId)
        {
            _accountService.DeleteAccount(accountId);
        }

        /// <summary>
        /// Updates existing account
        /// </summary>
        /// <param name="updatedAccount"></param>
        public void UpdateAccount(Account updatedAccount)
        {
            _accountService.UpdateAccount(updatedAccount);
        }

        /// <summary>
        /// Get account specified by id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public Account GetAccount(Guid accountId)
        {
            return _accountService.GetAccount(accountId);
        }

        /// <summary>
        /// List filtered accounts
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<Account> ListAccounts(string accountName, PageInfo pageInfo)
        {
            var filters = FilterFactory.GetAccountFilters(accountName);
            return _accountService.ListAccounts(filters, FilterFactory.GetPageAndOrderable<AccountModel>(pageInfo));
        }

        /// <summary>
        /// Attaches account with given ID to user with given access type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accountId"></param>
        /// <param name="accessType"></param>
        public void AttachAccountToUser(Guid userId, Guid accountId, AccountAccessType accessType)
        {
            _accountService.AttachAccountToUser(userId, accountId, accessType);
        }
        #endregion
    }
}
