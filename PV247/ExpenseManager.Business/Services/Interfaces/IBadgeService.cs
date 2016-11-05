using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.Services.Interfaces
{
    public interface IBadgeService
    {
        void CreateBadge(Badge badge);
        void EditBadge(Badge badge);
        void DeleteBadge(int badgeId);
        Badge GetBadge(int badgeId);
        IEnumerable<Badge> ListBadges(BadgeFilter filter);
        void AchieveBadge(Badge badge, AccountModel account);
    }
}
