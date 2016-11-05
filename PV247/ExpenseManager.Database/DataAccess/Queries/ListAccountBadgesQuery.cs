using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    public class ListAccountBadgesQuery : ExpenseManagerQuery<AccountBadgeModel>
    {
        public ListAccountBadgesQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        public AccountBadgeFilter Filter { get; set; }

        protected override IQueryable<AccountBadgeModel> GetQueryable()
        {
            IQueryable<AccountBadgeModel> accountBadges = Context.AccountBadges;

            if (Filter == null)
            {
                return accountBadges;
            }
            if (Filter.AccountId != null)
            {
                accountBadges = accountBadges.Where(plan => plan.AccountId == Filter.AccountId.Value);
            }
            if (Filter.BadgeId != null)
            {
                accountBadges = accountBadges.Where(plan => plan.BadgeId == Filter.BadgeId.Value);
            }
            if (Filter.AchievedFrom != null)
            {
                accountBadges = accountBadges.Where(plan => plan.Achieved >= Filter.AchievedFrom);
            }
            if (Filter.AchievedFrom != null)
            {
                accountBadges = accountBadges.Where(plan => plan.Achieved <= Filter.AchievedTo);
            }
            return accountBadges;
        }
    }
}
