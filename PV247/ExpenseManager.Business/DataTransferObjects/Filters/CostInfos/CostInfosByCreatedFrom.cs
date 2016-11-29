using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters cost info by its creation time
    /// </summary>
    public class CostInfosByCreatedFrom : IFilter<CostInfoModel>
    {
        /// <summary>
        /// Left edge of created range
        /// </summary>
        public DateTime? CreatedFrom { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="start"></param>
        public CostInfosByCreatedFrom(DateTime? start)
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
