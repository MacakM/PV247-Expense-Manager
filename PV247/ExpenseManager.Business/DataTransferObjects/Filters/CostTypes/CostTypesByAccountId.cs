using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostTypes
{
    internal class CostTypesByAccountId : IFilter<CostTypeModel>
    {
        /// <summary>
        /// Account id to be used in filter
        /// </summary>
        public readonly Guid? AccountId;

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostTypeModel> FilterQuery(IQueryable<CostTypeModel> queryable)
        {
            return AccountId!= null ? queryable.Where(costType => costType.AccountId == AccountId) : queryable;
        }

        public CostTypesByAccountId(Guid? accountId)
        {
            AccountId = accountId;
        }
    }
}
