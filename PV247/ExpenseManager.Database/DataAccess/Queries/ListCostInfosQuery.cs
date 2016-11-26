using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for cost infos.
    /// </summary>
    public class ListCostInfosQuery : ExpenseManagerQuery<CostInfoModel>
    {
        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public ListCostInfosQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
        protected override IQueryable<CostInfoModel> GetQueryable()
        {
            IQueryable<CostInfoModel> costInfos = Context.CostInfos.Include(nameof(CostInfoModel.Account)).Include(nameof(CostInfoModel.Type));

            return Filter == null ? costInfos : Filter.FilterQuery(costInfos);
        }
    }
}
