using System;
using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.Facades
{
    /// <summary>
    /// Provides access to user related functionality
    /// </summary>
    public class AccountFacade
    {
        private readonly IUserService _userService;

        private readonly IAccountService _accountService;
        
        /// <summary>
        /// Accont Facade constructor
        /// </summary>
        /// <param name="userService">User service</param>
        /// <param name="accountService">Accounet service</param>
        public AccountFacade(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
        }
        
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
        /// <param name="filters"></param>
        /// <param name="pageAndOrder"></param>
        /// <returns></returns>
        public List<User> ListUsers(List<IFilter<UserModel>> filters, IPageAndOrderable<UserModel> pageAndOrder)
        {
            return _userService.ListUsers(filters, pageAndOrder);
        }
        
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
        /// <param name="filters">Filters accounts</param>
        /// <param name="pageAndOrder"></param>
        /// <returns></returns>
        public List<Account> ListAccounts(List<IFilter<AccountModel>> filters, IPageAndOrderable<AccountModel> pageAndOrder)
        {
            return _accountService.ListAccounts(filters, pageAndOrder);
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
    }
}
