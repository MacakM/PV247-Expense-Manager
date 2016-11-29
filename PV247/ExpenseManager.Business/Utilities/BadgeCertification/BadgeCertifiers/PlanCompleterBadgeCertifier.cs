using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.Utilities.BadgeCertification.BadgeCertifiers
{
    /// <summary>
    /// Decides whether user account can be assigned PlanCompleter Badge
    /// </summary>
    public class PlanCompleterBadgeCertifier : BadgeCertifier
    {
        private const int RequiredPlansToAssignBadge = 5;

        /// <summary>
        /// Gets name of the corresponding badge
        /// </summary>
        /// <returns>Name of the corresponding badge</returns>
        internal override string GetBadgeName() => nameof(PlanCompleterBadgeCertifier)
                                                      .Replace(nameof(BadgeCertifier), string.Empty);

        /// <summary>
        /// Performs the real check if user account can be given a badge
        /// </summary>
        /// <param name="userAccount">User account to assign the badge to.</param>
        /// <returns>True if badge can be assigned</returns>
        protected override bool CanAssignBadgeCore(AccountModel userAccount)
        {
            return userAccount.Plans.Count(plan => plan.IsCompleted) >= RequiredPlansToAssignBadge;
        }
    }
}
