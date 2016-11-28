using System;
using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{ 
    /// <summary>
    /// Service handles AccountBadge entity operations
    /// </summary>
    public interface IAccountBadgeService : IService
    {
        /// <summary>
        /// Add new badge to account by creating new AccountBadge object in database
        /// </summary>
        /// <param name="accountBadge"></param>
        Guid CreateAccountBadge(AccountBadge accountBadge);

        /// <summary>
        /// Updates existing account badge
        /// </summary>
        /// <param name="updatedAccountBadge"></param>
        void UpdateAccountBadge(AccountBadge updatedAccountBadge);

        /// <summary>
        /// Deletes specified account badge
        /// </summary>
        /// <param name="accountBadgeId"></param>
        void DeleteAccountBadge(Guid accountBadgeId);

        /// <summary>
        /// Get account badge specified by id
        /// </summary>
        /// <param name="accountBadgeId"></param>
        /// <returns></returns>
        AccountBadge GetAccountBadge(Guid accountBadgeId);

        /// <summary>
        /// List filtered account badges
        /// </summary>
        /// <param name="filters">Filters account badgess</param>
        /// <param name="pageAndOrder"></param>
        /// <returns></returns>
        List<AccountBadge> ListAccountBadges(List<Filter<AccountBadge>> filters, PageAndOrderFilter pageAndOrder);
    }
}
