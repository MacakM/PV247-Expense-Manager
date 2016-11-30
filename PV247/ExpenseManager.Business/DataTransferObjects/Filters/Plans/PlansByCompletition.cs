using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Plans
{
    /// <summary>
    /// Filter plans by its completetions
    /// </summary>
    internal class PlansByCompletition : IFilter<PlanModel>
    {
        /// <summary>
        /// If plan is completed
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<PlanModel> FilterQuery(IQueryable<PlanModel> queryable)
        {
            return queryable.Where(plan => plan.IsCompleted == IsCompleted);
        }
    }
}