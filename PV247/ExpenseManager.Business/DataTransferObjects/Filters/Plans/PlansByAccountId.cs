using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Plans
{
    /// <summary>
    /// Filters plans by account id
    /// </summary>
    internal class PlansByAccountId : FilterValueBase<PlanModel, Guid>
    {
        public override Expression<Func<PlanModel, bool>> GetWhereCondition(Guid value)
            => plan => plan.AccountId == value;
    }
}

