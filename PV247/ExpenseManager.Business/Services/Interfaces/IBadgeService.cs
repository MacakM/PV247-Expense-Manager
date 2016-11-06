using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    public interface IBadgeService
    {
        void CreateBadge(Badge badge);
        void UpdateBadge(Badge badge);
        void DeleteBadge(int badgeId);
        Badge GetBadge(int badgeId);
        List<Badge> ListBadges(BadgeFilter filter);
        void AchieveBadge(Badge badge, Account account);
    }
}
