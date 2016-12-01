using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.Utilities.BadgeCertification.BadgeCertifiers
{
    /// <summary>
    /// Decides whether user account can be assigned PlanCompleter Badge
    /// </summary>
    public class PlanCompleterBadgeCertifier : IBadgeCertifier
    {
        private const int RequiredPlansToAssignBadge = 5;

        /// <summary>
        /// Gets name of the corresponding badge
        /// </summary>
        /// <returns>Name of the corresponding badge</returns>
        public string GetBadgeName() => nameof(PlanCompleterBadgeCertifier)
                                                      .Replace("BadgeCertifier", string.Empty);

        /// <summary>
        /// Performs the real check if user account can be given a badge
        /// </summary>
        /// <param name="userAccount">User account to assign the badge to.</param>
        /// <returns>True if badge can be assigned</returns>
        public bool CanAssignBadge(AccountModel userAccount)
        {
            if (userAccount.Badges.Any(badge => badge.Badge.Name.Equals(GetBadgeName())))
            {
                return false;
            }
            return userAccount.Plans.Count(plan => plan.IsCompleted) >= RequiredPlansToAssignBadge;
        }
    }
}
