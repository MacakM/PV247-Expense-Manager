using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for user's plans.
    /// </summary>
    internal class ListPlansQuery : ExpenseManagerQuery<PlanModel>
    {
        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">unitOfWork provider</param>
        internal ListPlansQuery(IUnitOfWorkProvider provider) : base(provider) { }

        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
        protected override IQueryable<PlanModel> GetQueryable()
        {
            return ApplyFilters(Context.Plans.Include(nameof(PlanModel.Account)).Include(nameof(PlanModel.PlannedType)));
        }
    }
}
