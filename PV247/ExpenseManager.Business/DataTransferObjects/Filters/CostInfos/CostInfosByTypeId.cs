using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters by planned type id
    /// </summary>
    public class CostInfosByTypeId : IFilter<CostInfoModel>
    {
        /// <summary>
        /// Type id to be filtered with
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// Filter construcotr
        /// </summary>
        /// <param name="plannedTypeId"></param>
        public CostInfosByTypeId(Guid plannedTypeId)
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
