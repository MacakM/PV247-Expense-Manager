using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Badges
{
    /// <summary>
    /// Filters by badge name
    /// </summary>
    internal class BadgesByName : IFilter<BadgeModel>
    {
        /// <summary>
        /// Name of Badge
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Filters given query
        /// </summary>
        /// <param name="queryable">Query to be filtered</param>
        public IQueryable<BadgeModel> FilterQuery(IQueryable<BadgeModel> queryable)
        {
            return Name != null ? queryable.Where(badge => badge.Name.Contains(Name)) : queryable;
        }

        public BadgesByName(string name)
        {
            Name = name;
        }
    }
}
