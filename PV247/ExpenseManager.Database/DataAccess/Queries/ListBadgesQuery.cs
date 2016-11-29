using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for badges.
    /// </summary>
    public class ListBadgesQuery : ExpenseManagerQuery<BadgeModel>
    {
        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public ListBadgesQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
        protected override IQueryable<BadgeModel> GetQueryable()
        {
            return ApplyFilters(Context.Badges);
        }
    }
}
