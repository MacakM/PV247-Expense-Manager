using System;
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
            IQueryable<AccountBadgeModel> accountBadges = Context.AccountBadges.Include(nameof(AccountBadgeModel.Account)).Include(nameof(AccountBadgeModel.Badge));

            if (Filter == null)
            {
                return accountBadges;
            }
            if (Filter.AccountId != null)
            {
                accountBadges = accountBadges.Where(plan => plan.AccountId == Filter.AccountId.Value);
            }
            if (!string.IsNullOrEmpty(Filter.AccountName))
            {
                accountBadges = Filter.DoExactMatch ? accountBadges.Where(accountBadge => accountBadge.Account.Name.Equals(Filter.AccountName)) : accountBadges.Where(accountBadge => accountBadge.Account.Name.Contains(Filter.AccountName));
            }
            if (!string.IsNullOrEmpty(Filter.BadgeDescription))
            {
                accountBadges = Filter.DoExactMatch ? accountBadges.Where(accountBadge => accountBadge.Badge.Description.Equals(Filter.BadgeDescription)) : accountBadges.Where(accountBadge => accountBadge.Badge.Description.Contains(Filter.BadgeDescription));
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
            if (Filter.OrderByDesc == null || string.IsNullOrEmpty(Filter.OrderByPropertyName))
            {
                return accountBadges;
            }
            System.Reflection.PropertyInfo prop = typeof(AccountBadgeModel).GetProperty(Filter.OrderByPropertyName);
            if (prop == null)
            {
                return accountBadges.Take(Filter.PageSize);
            }
            accountBadges = Filter.OrderByDesc.Value ? accountBadges.OrderByDescending(x => prop.GetValue(x, null)) : accountBadges.OrderBy(x => prop.GetValue(x, null));
            if (Filter.PageNumber != null)
            {
                accountBadges = accountBadges.Skip(Math.Max(0, Filter.PageNumber.Value - 1) * Filter.PageSize);
            }
            return accountBadges.Take(Filter.PageSize);
        }
    }
}
