using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Plans
{
    /// <summary>
    /// Filter plans based on planned type
    /// </summary>
    internal class PlansByType : IFilter<PlanModel>
    {
        /// <summary>
        /// Plan type to be used in filter
        /// </summary>
        public readonly PlanTypeModel? PlanType;

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<PlanModel> FilterQuery(IQueryable<PlanModel> queryable)
        {
            return PlanType != null ? queryable.Where(plan => plan.PlanType == PlanType) : queryable;
        }

        public PlansByType(PlanTypeModel? planType)
        {
            PlanType = planType;
        }
    }
}
