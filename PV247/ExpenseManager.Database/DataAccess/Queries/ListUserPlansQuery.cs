using System.Linq;
using ExpenseManager.Contract.DTOs;
using ExpenseManager.Contract.DTOs.Filters;
using AutoMapper.QueryableExtensions;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for user's plans.
    /// </summary>
    public class ListUserPlansQuery : ExpenseManagerQuery<PlanDTO>
    {
        /// <summary>
        /// Plan filter.
        /// </summary>
        public PlanFilterDTO Filter { get; set; }

        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public ListUserPlansQuery(IUnitOfWorkProvider provider) : base(provider) { }

        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
        protected override IQueryable<PlanDTO> GetQueryable()
        {
            IQueryable<Plan> plans = Context.Plans;
            if (Filter.UserId > 0)
            {
                plans = plans.Where(plan => plan.UserId == Filter.UserId);
            }

            // TODO add other filter criteria, ...

            return plans.ProjectTo<PlanDTO>();
        }
    }
}
