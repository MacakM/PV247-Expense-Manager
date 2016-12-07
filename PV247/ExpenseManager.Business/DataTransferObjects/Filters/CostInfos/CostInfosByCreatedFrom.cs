using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters cost info by its creation time
    /// </summary>
    internal class CostInfosByCreatedFrom : IFilter<CostInfoModel>
    {
        /// <summary>
        /// Left edge of created range
        /// </summary>
        public readonly DateTime? CreatedFrom;

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return CreatedFrom!= null ? queryable.Where(costInfo => costInfo.Created >= CreatedFrom) : queryable;
        }

        public CostInfosByCreatedFrom(DateTime? createdFrom)
        {
            CreatedFrom = createdFrom;
        }
    }
}
