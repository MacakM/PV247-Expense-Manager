using System.Collections.Generic;

namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class Account : BusinessObject<int>
    {
        /// <summary>
        /// Name of the account.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// List of users that have access to this account.
        /// </summary>
        public List<User> Users { get; set; }
        /// <summary>
        /// User's costs.
        /// </summary>
        public List<CostInfo> Costs { get; set; }
        /// <summary>
        /// All plans of the user.
        /// </summary>
        public List<Plan> Plans { get; set; }
        /// <summary>
        /// All badges of the user.
        /// </summary>
        public List<AccountBadge> Badges { get; set; }
    }
}
