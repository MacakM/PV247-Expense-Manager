using System;
using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    public class ListBadgesQuery : ExpenseManagerQuery<BadgeModel>
    {
        public ListBadgesQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        public BadgeModelFilter Filter { get; set; }

        protected override IQueryable<BadgeModel> GetQueryable()
        {
            IQueryable<BadgeModel> badges = Context.Badges;

            if (Filter == null)
            {
                return badges;
            }
            if (!string.IsNullOrEmpty(Filter.Description))
            {
                badges = Filter.DoExactMatch ? badges.Where(badge => badge.Description.Equals(Filter.Description)) : badges.Where(badge => badge.Description.Contains(Filter.Description));
            }
            if (Filter.OrderByDesc == null || string.IsNullOrEmpty(Filter.OrderByPropertyName))
            {
                return badges;
            }
            System.Reflection.PropertyInfo prop = typeof(AccountBadgeModel).GetProperty(Filter.OrderByPropertyName);
            if (prop == null)
            {
                return badges.Take(Filter.PageSize);
            }
            badges = Filter.OrderByDesc.Value ? badges.OrderByDescending(x => prop.GetValue(x, null)) : badges.OrderBy(x => prop.GetValue(x, null));
            if (Filter.PageNumber != null)
            {
                badges = badges.Skip(Math.Max(0, Filter.PageNumber.Value - 1) * Filter.PageSize);
            }
            return badges.Take(Filter.PageSize);
        }
    }
}
