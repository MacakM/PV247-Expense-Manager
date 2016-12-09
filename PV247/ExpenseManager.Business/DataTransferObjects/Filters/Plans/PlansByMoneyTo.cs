using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Plans
{
    /// <summary>
    /// Filters plans by planned money
    /// </summary>
    internal class PlansByMoneyTo : FilterValueBase<PlanModel, decimal>
    {
        public override Expression<Func<PlanModel, bool>> GetWhereCondition(decimal value)
            => plan => plan.PlannedMoney <= value;
    }
}
