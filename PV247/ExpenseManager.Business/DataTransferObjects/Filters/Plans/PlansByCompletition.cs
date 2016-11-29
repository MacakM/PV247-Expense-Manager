using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Plans
{
    /// <summary>
    /// Filter plans by its completetions
    /// </summary>
    public class PlansByCompletition : IFilter<PlanModel>
    {
        /// <summary>
        /// If plan is completed
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="completed"></param>
        public PlansByCompletition(bool completed)
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