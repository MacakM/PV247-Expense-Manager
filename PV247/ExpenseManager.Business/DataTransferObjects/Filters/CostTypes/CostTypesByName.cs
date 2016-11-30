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
        public string Name { get; set; }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostTypeModel> FilterQuery(IQueryable<CostTypeModel> queryable)
        {
            return queryable.Where(costType => costType.Name.Contains(Name));
        }
    }
}
