using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Plans
{
    /// <summary>
    /// Filters plans based on deadline 
    /// </summary>
    internal class PlansByDeadlineFrom : IFilter<PlanModel>
    {
        /// <summary>
        /// Left edge of deadline range
        /// </summary>
        public readonly DateTime? DeadlineFrom;

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<PlanModel> FilterQuery(IQueryable<PlanModel> queryable)
        {
            return DeadlineFrom != null ? queryable.Where(plan => plan.Deadline >= DeadlineFrom) : queryable;
        }

        public PlansByDeadlineFrom(DateTime? deadlineFrom)
        {
            DeadlineFrom = deadlineFrom;
        }
    }
}