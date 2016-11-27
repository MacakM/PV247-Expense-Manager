using System;
using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.Utilities.BadgeCertification.BadgeCertifiers
{
    /// <summary>
    /// Defines contract for all Badge certifiers
    /// </summary>
    public abstract class BadgeCertifier
    {
        /// <summary>
        /// Gets name of the corresponding badge
        /// </summary>
        /// <returns>Name of the corresponding badge</returns>
        internal abstract string GetBadgeName();

        /// <summary>
        /// Assigns user corresponding badge (according to badge name) 
        /// if user does not own the badge already and all requirements 
        /// prescribed by corresponding implementation are met.
        /// </summary>
        /// <param name="userAccount">User account to assign the badge to.</param>
        /// <returns>True if badge can be assigned</returns>
        internal bool CanAssignBadge(AccountModel userAccount)
        {
            if (userAccount.Badges.Any(badge => badge.Badge.Name.Equals(GetBadgeName())))
            {
                return false;
            }
            return CanAssignBadgeCore(userAccount);
        }

        /// <summary>
        /// Performs the real check if user account can be given a badge
        /// </summary>
        /// <param name="userAccount">User account to assign the badge to.</param>
        /// <returns>True if badge can be assigned</returns>
        protected virtual bool CanAssignBadgeCore(AccountModel userAccount)
        {
            throw new NotImplementedException();
        }
    }
}
