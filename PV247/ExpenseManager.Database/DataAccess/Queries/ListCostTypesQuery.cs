using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    public class ListCostTypesQuery : ExpenseManagerQuery<CostTypeModel>
    {
        public ListCostTypesQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        public CostTypeFilter Filter { get; set; }

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

            return costTypes;
        }
    }
}
