using System;
using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for user's plans.
    /// </summary>
    public class ListPlansQuery : ExpenseManagerQuery<PlanModel>
    {
        /// <summary>
        /// Plan filter.
        /// </summary>
        public PlanModelFilter Filter { get; set; }

        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public ListPlansQuery(IUnitOfWorkProvider provider) : base(provider) { }

        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
        protected override IQueryable<PlanModel> GetQueryable()
        {
            IQueryable<PlanModel> plans = Context.Plans.Include(nameof(PlanModel.Account)).Include(nameof(PlanModel.PlannedType));
            if (Filter == null)
            {
                return plans;
            }
            if (!string.IsNullOrEmpty(Filter.AccountName))
            {
                plans = Filter.DoExactMatch ? plans.Where(plan => plan.Account.Name.Equals(Filter.AccountName)) : plans.Where(plan => plan.Account.Name.Contains(Filter.AccountName));
            }
            if (!string.IsNullOrEmpty(Filter.CostTypeName))
            {
                plans = Filter.DoExactMatch ? plans.Where(plan => plan.PlannedType.Name.Equals(Filter.CostTypeName)) : plans.Where(plan => plan.PlannedType.Name.Equals(Filter.CostTypeName));
            }
            if (!string.IsNullOrEmpty(Filter.Description))
            {
                plans = Filter.DoExactMatch ? plans.Where(plan => plan.Description.Equals(Filter.Description)) : plans.Where(plan => plan.Description.Contains(Filter.Description));
            }
            if (Filter.CostTypeId != null)
            {
                plans = plans.Where(plan => plan.PlannedType.Id == Filter.CostTypeId);
            }
            if (Filter.AccountId != null)
            {
                plans = plans.Where(plan => plan.AccountId == Filter.AccountId);
            }
            if (Filter.PlanType != null)
            {
                plans = plans.Where(plan => plan.PlanType == Filter.PlanType.Value);
            }
            if (Filter.IsCompleted != null)
            {
                plans = plans.Where(plan => plan.IsCompleted == Filter.IsCompleted.Value);
            }
            if (Filter.DeadlineFrom != null)
            {
                plans = plans.Where(plan => plan.Deadline >= Filter.DeadlineFrom.Value);
            }
            if (Filter.DeadlineTo != null)
            {
                plans = plans.Where(plan => plan.Deadline <= Filter.DeadlineTo.Value);
            }
            if (Filter.PlannedMoneyFrom != null)
            {
                plans = plans.Where(plan => plan.PlannedMoney >= Filter.PlannedMoneyFrom.Value);
            }
            if (Filter.PlannedMoneyTo != null)
            {
                plans = plans.Where(plan => plan.PlannedMoney <= Filter.PlannedMoneyTo.Value);
            }
            if (Filter.OrderByDesc == null || string.IsNullOrEmpty(Filter.OrderByPropertyName))
            {
                return plans;
            }
            System.Reflection.PropertyInfo prop = typeof(AccountBadgeModel).GetProperty(Filter.OrderByPropertyName);
            if (prop == null)
            {
                return plans.Take(Filter.PageSize);
            }
            plans = Filter.OrderByDesc.Value ? plans.OrderByDescending(x => prop.GetValue(x, null)) : plans.OrderBy(x => prop.GetValue(x, null));
            if (Filter.PageNumber != null)
            {
                plans = plans.Skip(Math.Max(0, Filter.PageNumber.Value - 1) * Filter.PageSize);
            }
            return plans.Take(Filter.PageSize);
        }
    }
}
