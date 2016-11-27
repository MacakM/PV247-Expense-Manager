using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.Plans
{
    /// <summary>
    /// Filter plans by its completetions
    /// </summary>
    public class PlanModelsByCompletition : IFilterModel<PlanModel>
    {
        /// <summary>
        /// If plan is completed
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="completed"></param>
        public PlanModelsByCompletition(bool completed)
        {
            IsCompleted = completed;
        }

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
