using System;
using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.CostInfos
{
    /// <summary>
    /// Filters by account id
    /// </summary>
    public class CostInfoModelsByAccountId : FilterModel<CostInfoModel>
    {
        /// <summary>
        /// Account id
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="accountId"></param>
        public CostInfoModelsByAccountId(Guid accountId)
        {
            AccountId = accountId;
        }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public override IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return queryable.Where(x => x.AccountId == AccountId);
        }
    }
}
