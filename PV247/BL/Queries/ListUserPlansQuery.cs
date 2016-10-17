using System.Data.Entity;
using System.Linq;
using AutoMapper.QueryableExtensions;
using BL.DTOs;
using BL.DTOs.Filters;
using BL.Infrastructure.Queries;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
    public class ListUserPlansQuery : ExpenseManagerQuery<PlanDTO>
    {
        public PlanFilterDTO Filter { get; set; }

        public ListUserPlansQuery(IUnitOfWorkProvider provider) : base(provider) { }

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
