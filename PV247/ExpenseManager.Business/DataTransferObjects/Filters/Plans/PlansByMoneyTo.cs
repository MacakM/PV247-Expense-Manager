using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Plans
{
    /// <summary>
    /// Filters plans by planned money
    /// </summary>
    internal class PlansByMoneyTo : IFilter<PlanModel>
    {
        /// <summary>
        /// Right edge of planned money range
        /// </summary>
        public readonly decimal? PlannedMoneyTo;

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<PlanModel> FilterQuery(IQueryable<PlanModel> queryable)
        {
            return PlannedMoneyTo != null ? queryable.Where(plan => plan.PlannedMoney <= PlannedMoneyTo) : queryable;
        }

        public PlansByMoneyTo(decimal? plannedMoneyTo)
        {
            PlannedMoneyTo = plannedMoneyTo;
        }
    }
}
