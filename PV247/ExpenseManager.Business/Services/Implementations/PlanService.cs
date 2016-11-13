using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// Service handles plan entity operations
    /// </summary>
    public class PlanService : ExpenseManagerQueryAndCrudServiceBase<PlanModel, int, Plan, PlanModelFilter>, IPlanService
    {

        private readonly CostInfoRepository _costInfoRepository;
        private readonly ListCostTypesQuery _costTypesQuery;
        private readonly CostTypeRepository _costTypeRepository;
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
        public PlanService(ExpenseManagerQuery<PlanModel, PlanModelFilter> query, ExpenseManagerRepository<PlanModel, int> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider, CostInfoRepository costInfoRepository, CostTypeRepository costTypeRepository, ListCostTypesQuery costTypesQuery) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
            _costInfoRepository = costInfoRepository;
            _costTypeRepository = costTypeRepository;
            _costTypesQuery = costTypesQuery;
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
            Query.Filter = ExpenseManagerMapper.Map<PlanModelFilter>(filter);
            return GetList().ToList();
        }
   
        /// <summary>
        /// Transfers plan into cost
        /// </summary>
        /// <param name="plan"></param>
        public void ClosePlan(Plan plan)
        {
            if (plan.PlanType != PlanType.Save)
            {
                plan.IsCompleted = true;
                CloneToCost(plan);
                Save(plan);
            }
        }

        private void CloneToCost(Plan plan)
        {

            CostTypeModel costType = new CostTypeModel();
            costType.Name = plan.Description;
            _costTypeRepository.Insert(costType);
            _costTypesQuery.Filter = new CostTypeModelFilter() {Name = plan.Description, DoExactMatch = true};
            var type = _costTypesQuery.Execute().Single();

            CostInfoModel costInfo = new CostInfoModel();
            if (plan.PlannedMoney != null) costInfo.Money = plan.PlannedMoney.Value;
            if (plan.AccountId != null) costInfo.AccountId = plan.AccountId.Value;
            costInfo.TypeId = type.Id;
            costInfo.Created = DateTime.Now;
            costInfo.IsIncome = false;
            costInfo.Description = plan.Description;
            costInfo.PeriodicMultiplicity = 0;
            costInfo.Periodicity = PeriodicityModel.None;
            
            _costInfoRepository.Insert(costInfo);
        }

        /// <summary>
        /// Lists all plans, that can be closed by user
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="accountBalance"></param>
        /// <returns></returns>
        public List<Plan> ListAllCloseablePlans(int accountId, decimal accountBalance)
        {
             Query.Filter = new PlanModelFilter {AccountId = accountId, PlannedMoneyFrom = accountBalance, PlanType = PlanTypeModel.Save};
            return GetList().ToList();
        }
        /// <summary>
        /// Check all MaxSpent plans and in they at deadline and accomplished set em as completed
        /// </summary>
        public void CheckAllMaxSpendDeadlines()
        {
            
        }
    }
}
