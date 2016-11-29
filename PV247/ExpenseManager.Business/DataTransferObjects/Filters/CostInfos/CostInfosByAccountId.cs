using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters by account id
    /// </summary>
    public class CostInfosByAccountId : IFilter<CostInfoModel>
    {
        /// <summary>
        /// Account id
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="accountId"></param>
        public CostInfosByAccountId(Guid accountId)
        {
            AccountId = accountId;
        }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return queryable.Where(x => x.AccountId == AccountId);
        }
    }
}