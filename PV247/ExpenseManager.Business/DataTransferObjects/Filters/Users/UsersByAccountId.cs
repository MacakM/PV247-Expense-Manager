using System;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Users
{
    /// <summary>
    /// Filters by account id
    /// </summary>
    public class UsersByAccountId : IFilter<User>
    {
        /// <summary>
        /// Specifies account id to filter with
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Filters by account id
        /// </summary>
        /// <param name="accountId"></param>
        public UsersByAccountId(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
