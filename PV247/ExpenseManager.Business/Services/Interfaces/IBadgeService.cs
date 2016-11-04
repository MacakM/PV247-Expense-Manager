using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    public interface IBadgeService
    {
        void CreateBadge(Badge badge);
        void EditBadge(Badge badge);
        void DeleteBadge(int badgeId);
        Badge GetBadge(int badgeId);
        IEnumerable<Badge> ListBadges(BadgeFilter filter, int requiredPage = 1);

        void AchieveBadge(Badge badge, AccountModel account);
    }
}
