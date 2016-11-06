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
    /// <summary>
    /// Service handles plan entity operations
    /// </summary>
    public class PlanService : ExpenseManagerQueryAndCrudServiceBase<PlanModel, int, ListPlansQuery, Plan, PlanModelFilter>, IPlanService
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
        /// Creates new plan in databse
        /// </summary>
        /// <param name="plan">Object to be saved to database</param>
        public void CreatePlan(Plan plan)
        {
            Save(plan);
        }
        /// <summary>
        /// Updates plan, must have id of updated plan!
        /// </summary>
        /// <param name="planUpdated">Plan object with id of existing plan</param>
        public void UpdatePlan(Plan planUpdated)
        {
           Save(planUpdated);
        }
        /// <summary>
        /// Deletes plen with specified id
        /// </summary>
        /// <param name="planId">Unique id of deleted plan</param>
        public void DeletePlan(int planId)
        {
            Delete(planId);
        }
        /// <summary>
        /// Get specific plan specified by unique id
        /// </summary>
        /// <param name="planId">Unique id of plan</param>
        /// <returns></returns>
        public Plan GetPlan(int planId)
        {
            return GetDetail(planId);
        }
        /// <summary>
        /// Lists all plans that match filters criterias
        /// </summary>
        /// <param name="filter">Filters plans</param>
        /// <returns></returns>
        public List<Plan> ListPlans(PlanFilter filter)
        {
            Query.Filter = Mapper.Map<PlanModelFilter>(filter);
            return GetList().ToList();
        }
        /// <summary>
        /// Lists all plans, that can be closed by user
        /// </summary>
        /// <returns>List of plans</returns>
        public List<Plan> ListAllCloseablePlans()
        {
            throw new NotImplementedException();
        }

     
    }
}
