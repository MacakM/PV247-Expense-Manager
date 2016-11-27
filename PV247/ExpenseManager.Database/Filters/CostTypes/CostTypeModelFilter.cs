using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.CostTypes
{
    /// <summary>
    /// Filter userd in queries in order to get cost types with specifies parameters
    /// </summary>
    public class CostTypeModelFilterModel : IFilterModel<CostTypeModel>
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
        public IQueryable<CostTypeModel> FilterQuery(IQueryable<CostTypeModel> queryable)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                queryable = DoExactMatch ? queryable.Where(costType => costType.Name.Equals(Name)) : queryable.Where(costType => costType.Name.Contains(Name));
            }
            return queryable;
        }
    }
}
