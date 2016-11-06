using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    public interface IAccountBadgeService
    {
        void CreateAccountBadge(AccountBadge accountBadge);
        void UpdateAccountBadge(AccountBadge updatedAccountBadge);
        void DeleteAccountBadge(int accountBadgeId);
        AccountBadge GetAccountBadge(int accountBadgeId);
        List<AccountBadge> ListAccountBadges(AccountBadgeFilter filter);
    }
}
