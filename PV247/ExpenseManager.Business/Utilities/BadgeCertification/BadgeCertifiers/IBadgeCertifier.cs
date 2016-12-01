using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.Utilities.BadgeCertification.BadgeCertifiers
{
    public interface IBadgeCertifier
    {
        /// <summary>
        /// Gets name of the corresponding badge
        /// </summary>
        /// <returns>Name of the corresponding badge</returns>
        string GetBadgeName();

        /// <summary>
        /// Assigns user corresponding badge (according to badge name) 
        /// if user does not own the badge already and all requirements 
        /// prescribed by corresponding implementation are met.
        /// </summary>
        /// <param name="userAccount">User account to assign the badge to.</param>
        /// <returns>True if badge can be assigned</returns>
        bool CanAssignBadge(AccountModel userAccount);
    }
}