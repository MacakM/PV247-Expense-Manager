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
        public PlanFilter Filter { get; set; }

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
            IQueryable<PlanModel> plans = Context.Plans;
            if (Filter == null)
            {
                return plans;
            }
            if (Filter.AccountId != null)
            {
                plans = plans.Where(plan => plan.AccountId == Filter.AccountId);
            }
            if (Filter.PlanType != null)
            {
                plans = plans.Where(plan => plan.PlanType == Filter.PlanType.Value);
            }
            if (Filter.Description != null)
            {
                plans = plans.Where(plan => plan.Description.Contains(Filter.Description));
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

            return plans;
        }
    }
}
