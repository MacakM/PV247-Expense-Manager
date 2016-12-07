using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Plans
{
    /// <summary>
    /// Filters plans by account id
    /// </summary>
    internal class PlansByAccountId : IFilter<PlanModel>
    {
        /// <summary>
        /// Account id to be used in filter
        /// </summary>
        public readonly Guid? AccountId;

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<PlanModel> FilterQuery(IQueryable<PlanModel> queryable)
        {
            return AccountId != null ? queryable.Where(plan => plan.AccountId == AccountId) : queryable;
        }

        public PlansByAccountId(Guid? accountId)
        {
            AccountId = accountId;
        }
    }
}

