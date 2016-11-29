using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Badges
{
    /// <summary>
    /// Filters by badge name
    /// </summary>
    public class BadgesByName : IFilter<BadgeModel>
    {
        /// <summary>
        /// Name of Badge
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Determines if Equals() or Contains() should be used when matching string parameters
        /// </summary>
        public bool DoExactMatch { get; set; }

        /// <summary>
        /// Constructor of filter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="doExactMatch"></param>
        public BadgesByName(string name, bool doExactMatch)
        {
            Name = name;
            DoExactMatch = doExactMatch;
        }

        /// <summary>
        /// Filters given query
        /// </summary>
        /// <param name="queryable">Query to be filtered</param>
        public IQueryable<BadgeModel> FilterQuery(IQueryable<BadgeModel> queryable)
        {
            return DoExactMatch ? queryable.Where(badge => badge.Name.Equals(Name)) : queryable.Where(badge => badge.Name.Contains(Name));
        }
    }
}
