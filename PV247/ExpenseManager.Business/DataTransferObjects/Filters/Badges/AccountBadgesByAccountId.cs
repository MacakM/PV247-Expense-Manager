using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Badges
{
    internal class AccountBadgesByAccountId : IFilter<AccountBadgeModel>
    {

        public Guid AccountId { get; set; }

        public IQueryable<AccountBadgeModel> FilterQuery(IQueryable<AccountBadgeModel> queryable)
        {
            return queryable.Where(accountBadge => accountBadge.AccountId == AccountId);
        }
    }
}
