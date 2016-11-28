using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.CostTypes
{
    /// <summary>
    /// Filters by name
    /// </summary>
    public class CostTypeModelsByName : FilterModel<CostTypeModel>
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
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="doExactMatch"></param>
        public CostTypeModelsByName(string name, bool doExactMatch)
        {
            Name = name;
            DoExactMatch = doExactMatch;
        }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public override IQueryable<CostTypeModel> FilterQuery(IQueryable<CostTypeModel> queryable)
        {
            return DoExactMatch? queryable.Where(costType => costType.Name.Equals(Name)) : queryable.Where(costType => costType.Name.Contains(Name));
        }
    }
}
