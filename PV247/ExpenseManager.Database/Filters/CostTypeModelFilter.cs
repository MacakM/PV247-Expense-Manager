using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters
{
    /// <summary>
    /// Filter userd in queries in order to get cost types with specifies parameters
    /// </summary>
    public class CostTypeModelFilter : FilterModelBase<CostTypeModel>
    {
        /// <summary>
        /// Used for filtering based on cost type name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Determines if Equals() or Contains() should be used when matching string parameters
        /// </summary>
        public bool DoExactMatch { get; set; }

        /// <summary>
        /// Filters given query
        /// </summary>
        /// <param name="queryable">Query to be filtered</param>
        public override IQueryable<CostTypeModel> FilterQuery(IQueryable<CostTypeModel> queryable)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                queryable = DoExactMatch ? queryable.Where(costType => costType.Name.Equals(Name)) : queryable.Where(costType => costType.Name.Contains(Name));
            }
            if (OrderByDesc == null || string.IsNullOrEmpty(OrderByPropertyName))
            {
                return queryable;
            }
            System.Reflection.PropertyInfo prop = typeof(CostTypeModel).GetProperty(OrderByPropertyName);
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
