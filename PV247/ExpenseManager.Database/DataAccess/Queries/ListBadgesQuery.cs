using System;
using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for badges.
    /// </summary>
    public class ListBadgesQuery : ExpenseManagerQuery<BadgeModel, BadgeModelFilter>
    {
        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public ListBadgesQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }
        /// <summary>
        /// Badge filter
        /// </summary>
        public override BadgeModelFilter Filter { get; set; }
        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
        protected override IQueryable<BadgeModel> GetQueryable()
        {
            IQueryable<BadgeModel> badges = Context.Badges;

            if (Filter == null)
            {
                return badges;
            }
            if (!string.IsNullOrEmpty(Filter.Name))
            {
                badges = Filter.DoExactMatch ? badges.Where(badge => badge.Name.Equals(Filter.Name)) : badges.Where(badge => badge.Name.Contains(Filter.Name));
            }
            if (!string.IsNullOrEmpty(Filter.Description))
            {
                badges = Filter.DoExactMatch ? badges.Where(badge => badge.Description.Equals(Filter.Description)) : badges.Where(badge => badge.Description.Contains(Filter.Description));
            }
            if (Filter.OrderByDesc == null || string.IsNullOrEmpty(Filter.OrderByPropertyName))
            {
                return badges;
            }
            System.Reflection.PropertyInfo prop = typeof(BadgeModel).GetProperty(Filter.OrderByPropertyName);
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
