using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Services.Interfaces;

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
        /// 
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="accountService"></param>
        public AccountFacade(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
        }

        #region User CRUD

        /// <summary>
        /// Registers user according to provided information
        /// </summary>
        /// <param name="userRegistration">User registration information</param>
        /// <param name="createAccount"></param>
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
        public void DeleteUser(int userId)
        {
            _userService.DeleteUser(userId);
        }
        /// <summary>
        /// Get specific user that had id == userId
        /// </summary>
        /// <param name="userId">Unique user identifier</param>
        /// <returns>One user with id == userId</returns>
        public User GetUser(int userId)
        {
            return _userService.GetUser(userId);
        }
        /// <summary>
        /// List users that match parameters given in filter 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<User> ListUsers(UserFilter filter)
        {
            return _userService.ListUsers(filter);
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
        public void CreateAccount(int userId)
        {
            _accountService.CreateAccount(userId);
        }

        /// <summary>
        /// Deletes account by specified unique id
        /// </summary>
        /// <param name="accountId"></param>
        public void DeleteAccount(int accountId)
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
        public Account GetAccount(int accountId)
        {
            return _accountService.GetAccount(accountId);
        }
        /// <summary>
        /// List filtered accounts
        /// </summary>
        /// <param name="filter">Filters accounts</param>
        /// <returns></returns>
        public List<Account> ListAccounts(AccountFilter filter)
        {
            return _accountService.ListAccounts(filter);
        } 
        #endregion
    }
}
