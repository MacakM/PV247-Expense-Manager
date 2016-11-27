using System;
using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    /// <summary>
    /// Service handles Badge entity operations
    /// </summary>
    public interface IBadgeService : IService
    {
        /// <summary>
        /// Creates new Badge object in database
        /// </summary>
        /// <param name="badge">new Badge</param>
        Guid CreateBadge(Badge badge);

        /// <summary>
        /// Updates existing badge in database
        /// </summary>
        /// <param name="badge"></param>
        void UpdateBadge(Badge badge);

        /// <summary>
        /// Deletes badge specified by id
        /// </summary>
        /// <param name="badgeId"></param>
        void DeleteBadge(Guid badgeId);

        /// <summary>
        /// Get specific badge by unique id
        /// </summary>
        /// <param name="badgeId"></param>
        /// <returns></returns>
        Badge GetBadge(Guid badgeId);

        /// <summary>
        /// Lists filtered badges
        /// </summary>
        /// <param name="filters">Filters badges</param>
        /// <param name="pageAndOrder"></param>
        /// <returns></returns>
        List<Badge> ListBadges(List<Badge> filters, PageAndOrderFilter pageAndOrder);

        /// <summary>
        /// Check all accounts if they dont deserve some badges
        /// </summary>
        void CheckBadgesRequirements();
    }
}
