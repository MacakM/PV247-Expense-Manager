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

        public BadgeFilter Filter { get; set; }

        protected override IQueryable<BadgeModel> GetQueryable()
        {
            IQueryable<BadgeModel> badges = Context.Badges;

            if (Filter == null)
            {
                return badges;
            }
            if (string.IsNullOrEmpty(Filter.Description))
            {
                badges = Filter.DoExactMatch ? badges.Where(badge => badge.Description.Equals(Filter.Description)) : badges.Where(badge => badge.Description.Contains(Filter.Description));
            }
            return badges;
        }
    }
}
