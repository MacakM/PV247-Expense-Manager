using System;
using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.CostInfos
{
    /// <summary>
    /// Filters by planned type id
    /// </summary>
    public class CostInfoModelsByPlannedTypeId : IFilterModel<CostInfoModel>
    {
        /// <summary>
        /// Type id to be filtered with
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// Filter construcotr
        /// </summary>
        /// <param name="plannedTypeId"></param>
        public CostInfoModelsByPlannedTypeId(Guid plannedTypeId)
        {
            TypeId = plannedTypeId;
        }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return queryable.Where(costInfo => costInfo.TypeId == TypeId);
        }
    }
}
