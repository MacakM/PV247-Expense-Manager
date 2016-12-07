using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters by time of cost info creation
    /// </summary>
    internal class CostInfosByCreatedTo : IFilter<CostInfoModel>
    {
        /// <summary>
        /// Right edge of created range
        /// </summary>
        public readonly DateTime? CreatedTo;

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return CreatedTo!= null ? queryable.Where(costInfo => costInfo.Created <= CreatedTo) : queryable;
        }

        public CostInfosByCreatedTo(DateTime? createdTo)
        {
            CreatedTo = createdTo;
        }
    }
}
