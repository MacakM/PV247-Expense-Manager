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
        public decimal PlannedMoneyTo { get; set; }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<PlanModel> FilterQuery(IQueryable<PlanModel> queryable)
        {
            return queryable.Where(plan => plan.PlannedMoney <= PlannedMoneyTo);
        }
    }
}
