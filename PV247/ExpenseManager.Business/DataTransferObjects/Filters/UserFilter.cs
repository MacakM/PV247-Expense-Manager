using ExpenseManager.Business.DataTransferObjects.Enums;

namespace ExpenseManager.Business.DataTransferObjects.Filters
{
    /// <summary>
    /// Filter userd in queries in order to get users with specifies parameters
    /// </summary>
    public class UserFilter : FilterBase
    {
        /// <summary>
        /// Specifies users name to filter with
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Specifies users email to filter with
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Specifies account id to filter with
        /// </summary>
        public int? AccountId { get; set; }
        /// <summary>
        /// Specifies account name to filter with
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Determines if Equals() or Contains() should be use while filtering with strings
        /// </summary>
        public bool DoExactMatch { get; set; }
        /// <summary>
        /// Specifies users access type to filter with
        /// </summary>
        public AccountAccessType? AccessType { get; set; }
    }
}
