using System;
using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for cost types.
    /// </summary>
    public class ListCostTypesQuery : ExpenseManagerQuery<CostTypeModel, CostTypeModelFilter>
    {
        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public ListCostTypesQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }
        /// <summary>
        /// Cost type filter
        /// </summary>
        public override CostTypeModelFilter Filter { get; set; }
        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
        protected override IQueryable<CostTypeModel> GetQueryable()
        {
            IQueryable<CostTypeModel> costTypes = Context.CostTypes;

            if (Filter == null)
            {
                return costTypes;
            }
            if (!string.IsNullOrEmpty(Filter.Name))
            {
                costTypes = Filter.DoExactMatch ? costTypes.Where(costType => costType.Name.Equals(Filter.Name)) : costTypes.Where(costType => costType.Name.Contains(Filter.Name));
            }
            if (Filter.OrderByDesc == null || string.IsNullOrEmpty(Filter.OrderByPropertyName))
            {
                return costTypes;
            }
            System.Reflection.PropertyInfo prop = typeof(AccountBadgeModel).GetProperty(Filter.OrderByPropertyName);
            if (prop == null)
            {
                return costTypes.Take(Filter.PageSize);
            }
            costTypes = Filter.OrderByDesc.Value ? costTypes.OrderByDescending(x => prop.GetValue(x, null)) : costTypes.OrderBy(x => prop.GetValue(x, null));
            if (Filter.PageNumber != null)
            {
                costTypes = costTypes.Skip(Math.Max(0, Filter.PageNumber.Value - 1) * Filter.PageSize);
            }
            return costTypes.Take(Filter.PageSize);
        }
    }
}
