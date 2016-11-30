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
        public string Name { get; set; }

        /// <summary>
        /// Filters given query
        /// </summary>
        /// <param name="queryable">Query to be filtered</param>
        public IQueryable<BadgeModel> FilterQuery(IQueryable<BadgeModel> queryable)
        {
            return queryable.Where(badge => badge.Name.Contains(Name));
        }
    }
}
