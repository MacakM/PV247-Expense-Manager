using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Plans
{
    /// <summary>
    /// Filter plans by its completetions
    /// </summary>
    internal class PlansByCompletition : FilterValueBase<PlanModel, bool>
    {
        public override Expression<Func<PlanModel, bool>> GetWhereCondition(bool value)
            => plan => plan.IsCompleted == value;
    }
}