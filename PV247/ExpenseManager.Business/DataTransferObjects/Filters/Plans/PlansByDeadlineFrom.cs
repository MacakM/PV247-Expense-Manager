using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Plans
{
    /// <summary>
    /// Filters plans based on deadline 
    /// </summary>
    internal class PlansByDeadlineFrom : FilterValueBase<PlanModel, DateTime>
    {
        public override Expression<Func<PlanModel, bool>> GetWhereCondition(DateTime value)
            => plan => plan.Deadline >= value;
    }
}