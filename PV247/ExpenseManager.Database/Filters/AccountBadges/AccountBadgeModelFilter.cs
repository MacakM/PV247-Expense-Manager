using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.AccountBadges
{
    /// <summary>
    /// Filter userd in queries in order to get account badges with specifies parameters
    /// </summary>
    public class AccountBadgeModelFilter : IFilter<AccountBadgeModel>
    {
        /// <summary>
        /// Account id to be filtered with
        /// </summary>
        public Guid? AccountId { get; set; }

        /// <summary>
        /// Account name to be filtered with
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        ///  Determines if Equals() or Contains() should be used when matching string parameters
        /// </summary>
        public bool DoExactMatch { get; set; }

        /// <summary>
        /// Badge id to be filtered with
        /// </summary>
        public Guid? BadgeId { get; set; }

        /// <summary>
        /// Badge description to be filtered with
        /// </summary>
        public string BadgeDescription { get; set; }

        /// <summary>
        /// Left edge of achieved time range
        /// </summary>
        public DateTime? AchievedFrom { get; set; }

        /// <summary>
        /// Right edge of achieved time range
        /// </summary>
        public DateTime? AchievedTo { get; set; }

        /// <summary>
        /// Filters given query
        /// </summary>
        /// <param name="queryable">Query to be filtered</param>
        public IQueryable<AccountBadgeModel> FilterQuery(IQueryable<AccountBadgeModel> queryable)
        {
            if (AccountId != null)
            {
                queryable = queryable.Where(plan => plan.AccountId == AccountId.Value);
            }
            if (!string.IsNullOrEmpty(AccountName))
            {
                queryable = DoExactMatch ? queryable.Where(accountBadge => accountBadge.Account.Name.Equals(AccountName)) : queryable.Where(accountBadge => accountBadge.Account.Name.Contains(AccountName));
            }
            if (!string.IsNullOrEmpty(BadgeDescription))
            {
                queryable = DoExactMatch ? queryable.Where(accountBadge => accountBadge.Badge.Description.Equals(BadgeDescription)) : queryable.Where(accountBadge => accountBadge.Badge.Description.Contains(BadgeDescription));
            }
            if (BadgeId != null)
            {
                queryable = queryable.Where(plan => plan.BadgeId == BadgeId.Value);
            }
            if (AchievedFrom != null)
            {
                queryable = queryable.Where(plan => plan.Achieved >= AchievedFrom.Value);
            }
            if (AchievedFrom != null)
            {
                queryable = queryable.Where(plan => plan.Achieved <= AchievedTo.Value);
            }
            return queryable;
        }
    }
}
