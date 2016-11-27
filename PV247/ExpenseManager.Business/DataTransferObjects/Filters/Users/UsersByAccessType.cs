using ExpenseManager.Business.DataTransferObjects.Enums;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Users
{
    /// <summary>
    /// Filter query by access type
    /// </summary>
    public class UsersByAccessType : IFilter<User>
    {
        /// <summary>
        /// Specifies users access type to filter with
        /// </summary>
        public AccountAccessType AccessType { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="accessType">Access type to be used in filter</param>
        public UsersByAccessType(AccountAccessType accessType)
        {
            AccessType = accessType;
        }
    }
}