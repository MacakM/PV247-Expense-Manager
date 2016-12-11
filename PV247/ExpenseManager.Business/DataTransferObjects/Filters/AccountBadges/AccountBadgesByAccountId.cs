using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.AccountBadges
{
    /// <summary>
    /// Filters account badges by account id
    /// </summary>
    internal class AccountBadgesByAccountId : FilterValueBase<AccountBadgeModel, Guid>
    { 
        public override Expression<Func<AccountBadgeModel, bool>> GetWhereCondition(Guid value)
            => accountBadge => accountBadge.AccountId == value;
    }
}
