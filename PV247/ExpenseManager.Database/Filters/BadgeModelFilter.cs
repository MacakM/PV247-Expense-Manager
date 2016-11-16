using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters
{

    /// <summary>
    /// Filter userd in queries in order to get badges with specifies parameters
    /// </summary>
    public class BadgeModelFilter : FilterModelBase<BadgeModel>
    {
        /// <summary>
        /// Name of Badge
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description to be filtered with
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Determines if Equals() or Contains() should be used when matching string parameters
        /// </summary>
        public bool DoExactMatch { get; set; }

        /// <summary>
        /// Filters given query
        /// </summary>
        /// <param name="queryable">Query to be filtered</param>
        public override IQueryable<BadgeModel> FilterQuery(IQueryable<BadgeModel> queryable)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                queryable = DoExactMatch ? queryable.Where(badge => badge.Name.Equals(Name)) : queryable.Where(badge => badge.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Description))
            {
                queryable = DoExactMatch ? queryable.Where(badge => badge.Description.Equals(Description)) : queryable.Where(badge => badge.Description.Contains(Description));
            }
            if (OrderByDesc == null || string.IsNullOrEmpty(OrderByPropertyName))
            {
                return queryable;
            }
            System.Reflection.PropertyInfo prop = typeof(BadgeModel).GetProperty(OrderByPropertyName);
            if (prop == null)
            {
                return queryable;
            }
            queryable = OrderByDesc.Value ? QueryOrderByHelper.OrderByDesc(queryable, OrderByPropertyName) : QueryOrderByHelper.OrderBy(queryable, OrderByPropertyName);
            if (PageNumber != null)
            {
                queryable = queryable.Skip(Math.Max(0, PageNumber.Value - 1) * PageSize);
            }
            return queryable.Take(PageSize);
        }
    }
}
