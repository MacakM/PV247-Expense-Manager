using System;
using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.CostInfos
{
    /// <summary>
    /// Filters cost info by its creation time
    /// </summary>
    public class CostInfoModelsByCreatedFrom : IFilterModel<CostInfoModel>
    {
        /// <summary>
        /// Left edge of created range
        /// </summary>
        public DateTime? CreatedFrom { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="start"></param>
        public CostInfoModelsByCreatedFrom(DateTime? start)
        {
            CreatedFrom = start;
        }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return queryable.Where(costInfo => costInfo.Created >= CreatedFrom);
        }
    }
}
