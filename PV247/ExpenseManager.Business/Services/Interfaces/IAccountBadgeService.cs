using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountBadgeService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountBadge"></param>
        void CreateAccountBadge(AccountBadge accountBadge);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedAccountBadge"></param>
        void UpdateAccountBadge(AccountBadge updatedAccountBadge);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountBadgeId"></param>
        void DeleteAccountBadge(int accountBadgeId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountBadgeId"></param>
        /// <returns></returns>
        AccountBadge GetAccountBadge(int accountBadgeId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<AccountBadge> ListAccountBadges(AccountBadgeFilter filter);
    }
}
