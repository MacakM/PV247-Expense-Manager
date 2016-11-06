using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    /// <summary>
    /// Service handles Badge entity operations
    /// </summary>
    public interface IBadgeService
    {
        /// <summary>
        /// Creates new Badge object in database
        /// </summary>
        /// <param name="badge">new Badge</param>
        void CreateBadge(Badge badge);
        /// <summary>
        /// Updates existing badge in database
        /// </summary>
        /// <param name="badge"></param>
        void UpdateBadge(Badge badge);
        /// <summary>
        /// Deletes badge cpecified by id
        /// </summary>
        /// <param name="badgeId"></param>
        void DeleteBadge(int badgeId);
        /// <summary>
        /// Get specific badge by unique id
        /// </summary>
        /// <param name="badgeId"></param>
        /// <returns></returns>
        Badge GetBadge(int badgeId);
        /// <summary>
        /// Lists filtered badges
        /// </summary>
        /// <param name="filter">Filters badges</param>
        /// <returns></returns>
        List<Badge> ListBadges(BadgeFilter filter);
        /// <summary>
        /// Check all accounts if they dont deserve some badges
        /// </summary>
        void CheckBadgesRequirements();
    }
}
