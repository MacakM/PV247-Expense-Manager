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
        public readonly Guid? TypeId;

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return TypeId != null ? queryable.Where(costInfo => costInfo.TypeId == TypeId) : queryable;
        }

        public CostInfosByTypeId(Guid? typeId)
        {
            TypeId = typeId;
        }
    }
}
