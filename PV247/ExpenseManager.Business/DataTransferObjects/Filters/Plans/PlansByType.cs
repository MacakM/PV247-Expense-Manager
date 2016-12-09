using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Plans
{
    /// <summary>
    /// Filter plans based on planned type
    /// </summary>
    internal class PlansByType : FilterValueBase<PlanModel, PlanTypeModel>
    {
        public override Expression<Func<PlanModel, bool>> GetWhereCondition(PlanTypeModel value)
            => plan => plan.PlanType == value;
    }
}