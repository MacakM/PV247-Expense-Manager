using System;
using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.Plans
{
    /// <summary>
    /// Filters plans by account id
    /// </summary>
    public class PlanModelsByAccountId : IFilterModel<PlanModel>
    {
        /// <summary>
        /// Account id to be used in filter
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="accountId">Account id</param>
        public PlanModelsByAccountId(Guid accountId)
        {
            AccountId = accountId;
        }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<PlanModel> FilterQuery(IQueryable<PlanModel> queryable)
        {
              return queryable.Where(plan => plan.AccountId == AccountId);
        }
    }
}
