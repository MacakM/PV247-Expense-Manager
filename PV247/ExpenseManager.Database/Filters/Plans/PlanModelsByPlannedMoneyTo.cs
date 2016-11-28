using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.Plans
{
    /// <summary>
    /// Filters plans by planned money
    /// </summary>
    public class PlanModelsByPlannedMoneyTo : FilterModel<PlanModel>
    {
        /// <summary>
        /// Right edge of planned money range
        /// </summary>
        public decimal PlannedMoneyTo { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="plannedMoneyTo"></param>
        public PlanModelsByPlannedMoneyTo(decimal plannedMoneyTo)
        {
            PlannedMoneyTo = plannedMoneyTo;
        }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public override IQueryable<PlanModel> FilterQuery(IQueryable<PlanModel> queryable)
        {
             return queryable.Where(plan => plan.PlannedMoney <= PlannedMoneyTo);
        }
    }
}
