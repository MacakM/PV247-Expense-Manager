using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostTypes
{
    /// <summary>
    /// Filters by name
    /// </summary>
    internal class CostTypesByName : IFilter<CostTypeModel>
    {
        /// <summary>
        /// Used for filtering based on cost type name
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostTypeModel> FilterQuery(IQueryable<CostTypeModel> queryable)
        {
            return Name != null ? queryable.Where(costType => costType.Name.Contains(Name)) : queryable;
        }

        public CostTypesByName(string name)
        {
            Name = name;
        }
    }
}
