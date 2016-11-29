using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Plans
{
    /// <summary>
    /// Filter plans based on planned type
    /// </summary>
    public class PlansByType : IFilter<PlanModel>
    {
        /// <summary>
        /// Plan type to be used in filter
        /// </summary>
        public PlanTypeModel PlanType { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="planType"></param>
        public PlansByType(PlanTypeModel planType)
        {
            PlanType = planType;
        }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<PlanModel> FilterQuery(IQueryable<PlanModel> queryable)
        {
            return queryable.Where(plan => plan.PlanType == PlanType);
        }
    }
}
