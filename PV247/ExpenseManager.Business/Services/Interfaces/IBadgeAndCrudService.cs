using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Business.DTOs;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    public interface IBadgeAndCrudService
    {
        void CreateBadge(BadgeDTO badgeDto);
        void EditBadge(BadgeDTO badgeDto);
        void DeleteBadge(int badgeId);
        BadgeDTO GetBadge(int badgeId);
        IEnumerable<BadgeDTO> ListBadges(BadgeFilter filter, int requiredPage = 1);

        void AchieveBadge(BadgeDTO badgeDto, Account account);
    }
}
