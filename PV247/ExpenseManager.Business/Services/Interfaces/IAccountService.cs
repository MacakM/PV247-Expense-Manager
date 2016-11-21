using System;
using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    /// <summary>
    /// Service handles Account entity operations
    /// </summary>
    public interface IAccountService : IService
    {
        /// <summary>
        /// Creates new account
        /// </summary>
        /// <param name="account"></param>
        Guid CreateAccount(Account account);
        /// <summary>
        /// Updates existing account
        /// </summary>
        /// <param name="updatedAccount"></param>
        void UpdateAccount(Account updatedAccount);
        /// <summary>
        /// Deletes account by specified unique id
        /// </summary>
        /// <param name="accountId"></param>
        void DeleteAccount(Guid accountId);
        /// <summary>
        /// Get account specified by id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Account GetAccount(Guid accountId);
        /// <summary>
        /// List filtered accounts
        /// </summary>
        /// <param name="filter">Filters accounts</param>
        /// <returns></returns>
        List<Account> ListAccounts(AccountFilter filter);
        /// <summary>
        /// Creates account for user with given id
        /// </summary>
        Guid CreateAccount(Guid userId);

        /// <summary>
        /// Attaches account with given ID to user with given access type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accountId"></param>
        /// <param name="accessType"></param>
        void AttachAccountToUser(Guid userId, Guid accountId, AccountAccessType accessType);
    }
}
