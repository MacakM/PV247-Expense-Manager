using System.Linq;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.Utilities.BadgeCertification.BadgeCertifiers
{
    /// <summary>
    /// Decides whether user account can be assigned PassionatePennyPincher Badge
    /// </summary>
    public class PassionatePennyPincherBadgeCertifier : IBadgeCertifier
    {
        private const decimal PlannedMoney = 20000;

        /// <summary>
        /// Gets name of the corresponding badge
        /// </summary>
        /// <returns>Name of the corresponding badge</returns>
        public string GetBadgeName() => nameof(PassionatePennyPincherBadgeCertifier)
                                                      .Replace("BadgeCertifier", string.Empty);

        /// <summary>
        /// Performs the real check if user account can be given a badge
        /// </summary>
        /// <param name="userAccount">User account to assign the badge to.</param>
        /// <returns>True if badge can be assigned</returns>
        public bool CanAssignBadge(AccountModel userAccount)
        {
            // At least 20 000 CZK within all completed plans
            if (userAccount.Badges.Any(badge => badge.Badge.Name.Equals(GetBadgeName())))
            {
                return false;
            }
            return userAccount.Plans
                .Where(plan => plan.PlanType.Equals(PlanType.Save) && plan.IsCompleted)
                .Sum(plan => plan.PlannedMoney) > PlannedMoney;
        }
    }
}
