using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    // TODO doc
    /// <summary>
    /// 
    /// </summary>
    public class PlanService : ExpenseManagerQueryAndCrudServiceBase<PlanModel, int, Plan, PlanModelFilter>, IPlanService
    {
        /// <summary>
        /// 
        /// </summary>
        protected override string[] EntityIncludes { get; } =
        {
            nameof(PlanModel.Account),
            nameof(PlanModel.PlannedType)
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="repository"></param>
        /// <param name="expenseManagerMapper"></param>
        /// <param name="unitOfWorkProvider"></param>
        public PlanService(ListPlansQuery query, ExpenseManagerRepository<PlanModel, int> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plan"></param>
        public void CreatePlan(Plan plan)
        {
            Save(plan);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="planUpdated"></param>
        public void UpdatePlan(Plan planUpdated)
        {
           Save(planUpdated);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="planId"></param>
        public void DeletePlan(int planId)
        {
            Delete(planId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public Plan GetPlan(int planId)
        {
            return GetDetail(planId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Plan> ListPlans(PlanFilter filter)
        {
            Query.Filter = Mapper.Map<PlanModelFilter>(filter);
            return GetList().ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        public void CheckAllPlansFulfillment()
        {
            throw new NotImplementedException();
        }
    }
}
