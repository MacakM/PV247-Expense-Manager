using System;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Plans
{
    /// <summary>
    /// Filters plans by account id
    /// </summary>
    public class PlansByAccountId : Filter<Plan>
    {
        /// <summary>
        /// Account id to be used in filter
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="accountId">Account id</param>
        public PlansByAccountId(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
