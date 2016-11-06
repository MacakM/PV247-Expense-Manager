using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    // TODO doc
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        void CreateAccount(Account account);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedAccount"></param>
        void UpdateAccount(Account updatedAccount);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        void DeleteAccount(int accountId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Account GetAccount(int accountId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<Account> ListAccounts(AccountFilter filter);
    }
}
