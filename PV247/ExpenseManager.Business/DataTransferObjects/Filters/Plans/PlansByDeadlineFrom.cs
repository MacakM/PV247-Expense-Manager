using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Plans
{
    /// <summary>
    /// Filters plans based on deadline 
    /// </summary>
    public class PlansByDeadlineFrom : IFilter<PlanModel>
    {
        /// <summary>
        /// Left edge of deadline range
        /// </summary>
        public DateTime? DeadlineFrom { get; set; }

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