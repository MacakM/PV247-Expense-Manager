using System;
using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.Plans
{
    /// <summary>
    /// Filters plans based on deadline 
    /// </summary>
    public class PlanModelsByDeadlineFrom : IFilterModel<PlanModel>
    {
        /// <summary>
        /// Left edge of deadline range
        /// </summary>
        public DateTime? DeadlineFrom { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="deadlineFrom"></param>
        public PlanModelsByDeadlineFrom(DateTime deadlineFrom)
        {
            DeadlineFrom = deadlineFrom;
        }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<PlanModel> FilterQuery(IQueryable<PlanModel> queryable)
        {
            return queryable.Where(plan => plan.Deadline >= DeadlineFrom);
        }
    }
}
