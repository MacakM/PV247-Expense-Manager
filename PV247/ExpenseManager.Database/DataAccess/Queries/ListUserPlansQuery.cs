using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for user's plans.
    /// </summary>
    public class ListUserPlansQuery : ExpenseManagerQuery<Plan>
    {
        /// <summary>
        /// Plan filter.
        /// </summary>
        public PlanFilter Filter { get; set; }

        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public ListUserPlansQuery(IUnitOfWorkProvider provider) : base(provider) { }

        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
        protected override IQueryable<Plan> GetQueryable()
        {
            IQueryable<Plan> plans = Context.Plans;
            if (Filter.AccountId > 0)
            {
                plans = plans.Where(plan => plan.AccountId == Filter.AccountId);
            }

            // TODO add other filter criteria, ...

            return plans;
        }
    }
}
