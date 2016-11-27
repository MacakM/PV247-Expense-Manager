using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.Utilities.BadgeCertification.BadgeCertifiers
{
    /// <summary>
    /// Defines contract for all Badge certifiers
    /// </summary>
    public interface IBadgeCertifier
    {
        /// <summary>
        /// Name of the badge to assign to user (TODO: check if this can be removed)
        /// </summary>
        string BadgeName { get; }

        /// <summary>
        /// Assigns user corresponding badge (according to badge name) 
        /// if user does not own the badge already and all requirements 
        /// prescribed by corresponding implementation are met.
        /// </summary>
        /// <param name="userAccount">User account to assign the badge to.</param>
        void AssignBadge(AccountModel userAccount);
    }
}
