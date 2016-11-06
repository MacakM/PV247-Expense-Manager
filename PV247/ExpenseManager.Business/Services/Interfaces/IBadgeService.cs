using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    // TODO doc
    /// <summary>
    /// 
    /// </summary>
    public interface IBadgeService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badge"></param>
        void CreateBadge(Badge badge);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badge"></param>
        void UpdateBadge(Badge badge);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badgeId"></param>
        void DeleteBadge(int badgeId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badgeId"></param>
        /// <returns></returns>
        Badge GetBadge(int badgeId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<Badge> ListBadges(BadgeFilter filter);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badge"></param>
        /// <param name="account"></param>
        void AchieveBadge(Badge badge, Account account);
        /// <summary>
        /// 
        /// </summary>
        void CheckBadgesRequirements();
    }
}
