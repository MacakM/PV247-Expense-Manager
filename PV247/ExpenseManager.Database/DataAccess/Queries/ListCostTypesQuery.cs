using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for cost types.
    /// </summary>
    public class ListCostTypesQuery : ExpenseManagerQuery<CostTypeModel>
    {
        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public ListCostTypesQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }
        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
        protected override IQueryable<CostTypeModel> GetQueryable()
        {
            IQueryable<CostTypeModel> costTypes = Context.CostTypes;

            return Filter == null ? costTypes : Filter.FilterQuery(costTypes);
        }
    }
}
