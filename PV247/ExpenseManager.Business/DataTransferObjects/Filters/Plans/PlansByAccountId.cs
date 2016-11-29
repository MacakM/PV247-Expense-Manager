using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Plans
{
    /// <summary>
    /// Filters plans by account id
    /// </summary>
    public class PlansByAccountId : IFilter<PlanModel>
    {
        /// <summary>
        /// Account id to be used in filter
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="accountId">Account id</param>
        public PlansByAccountId(Guid accountId)
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

