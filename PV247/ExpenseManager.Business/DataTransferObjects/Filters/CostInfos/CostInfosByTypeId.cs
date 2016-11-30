using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters by planned type id
    /// </summary>
    internal class CostInfosByTypeId : IFilter<CostInfoModel>
    {
        /// <summary>
        /// Type id to be filtered with
        /// </summary>
        public Guid TypeId { get; set; }

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
