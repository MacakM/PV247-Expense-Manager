using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for user's plans.
    /// </summary>
    public class ListPlansQuery : ExpenseManagerQuery<PlanModel>
    {

        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public ListPlansQuery(IUnitOfWorkProvider provider) : base(provider) { }

        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>D:\FI\3. Semestr\PV247\PV247\ExpenseManager.Database\DataAccess\Queries\ListPlansQuery.cs
        protected override IQueryable<PlanModel> GetQueryable()
        {
            IQueryable<PlanModel> plans = Context.Plans.Include(nameof(PlanModel.Account)).Include(nameof(PlanModel.PlannedType));
            return Filter == null ? plans : Filter.FilterQuery(plans);
        }
    }
}
