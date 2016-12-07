using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters by account id
    /// </summary>
    internal class CostInfosByAccountId : IFilter<CostInfoModel>
    {
        /// <summary>
        /// Account id
        /// </summary>
        public readonly Guid? AccountId;

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
             return AccountId == null ? queryable : queryable.Where(x => x.AccountId == AccountId);
        }

        public CostInfosByAccountId(Guid? accountId)
        {
            AccountId = accountId;
        }
    }
}