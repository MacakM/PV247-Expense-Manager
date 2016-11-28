using System;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters by account id
    /// </summary>
    public class CostInfosByAccountId : Filter<CostInfo>
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
    }
}