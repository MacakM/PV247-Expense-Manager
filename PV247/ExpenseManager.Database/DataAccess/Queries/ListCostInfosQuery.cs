using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for cost infos.
    /// </summary>
    internal class ListCostInfosQuery : ExpenseManagerQuery<CostInfoModel>
    {
        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">unitOfWork provider</param>
        internal ListCostInfosQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
        protected override IQueryable<CostInfoModel> GetQueryable()
        {
           return ApplyFilters(Context.CostInfos.Include(nameof(CostInfoModel.Account)).Include(nameof(CostInfoModel.Type)));
        }
    }
}
