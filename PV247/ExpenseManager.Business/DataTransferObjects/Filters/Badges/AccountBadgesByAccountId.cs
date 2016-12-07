using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Badges
{
    internal class AccountBadgesByAccountId : IFilter<AccountBadgeModel>
    {

        public readonly Guid? AccountId;

        public IQueryable<AccountBadgeModel> FilterQuery(IQueryable<AccountBadgeModel> queryable)
        {
            return AccountId == null ? queryable : queryable.Where(accountBadge => accountBadge.AccountId == AccountId);
        }

        public AccountBadgesByAccountId(Guid? accountId)
        {
            AccountId = accountId;
        }
    }
}
