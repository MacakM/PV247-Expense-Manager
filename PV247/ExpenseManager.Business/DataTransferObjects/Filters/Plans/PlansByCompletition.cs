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
        public readonly bool? IsCompleted;

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<PlanModel> FilterQuery(IQueryable<PlanModel> queryable)
        {
            return IsCompleted != null ? queryable.Where(plan => plan.IsCompleted == IsCompleted) : queryable;
        }

        public PlansByCompletition(bool? isCompleted)
        {
            IsCompleted = isCompleted;
        }
    }
}