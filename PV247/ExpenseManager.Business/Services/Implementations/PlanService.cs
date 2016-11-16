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
    public class PlanService : ExpenseManagerQueryAndCrudServiceBase<PlanModel, int, Plan>, IPlanService
    {

        private readonly CostInfoRepository _costInfoRepository;
        private readonly ListAccountsQuery _accountsQuery;
        private readonly ListCostInfosQuery _costInfosQuery;
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
        /// <param name="costInfoRepository"></param>
        /// <param name="accountsQuery"></param>
        /// <param name="costInfosQuery"></param>
        public PlanService(ExpenseManagerQuery<PlanModel> query, ExpenseManagerRepository<PlanModel, int> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider, CostInfoRepository costInfoRepository, ListAccountsQuery accountsQuery, ListCostInfosQuery costInfosQuery) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
            _costInfoRepository = costInfoRepository;
            _accountsQuery = accountsQuery;
            _costInfosQuery = costInfosQuery;
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
            if (plan.PlanType == PlanType.Save)
            {
                plan.IsCompleted = true;
                CloneToCost(plan);
                Save(plan);
            } 
            throw new ArgumentException("PlanType.Save is the right one.");
        }

        private void CloneToCost(Plan plan)
        {
           
            CostInfoModel costInfo = new CostInfoModel();

            if (plan.PlannedMoney != null) costInfo.Money = plan.PlannedMoney.Value;
            if (plan.AccountId != null) costInfo.AccountId = plan.AccountId.Value;
            if (plan.PlannedTypeId != null) costInfo.TypeId = plan.PlannedTypeId.Value;
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
             Query.Filter = new PlanModelFilter {AccountId = accountId, PlannedMoneyFrom = accountBalance, PlanType = PlanTypeModel.Save, IsCompleted = false};
            return GetList().ToList();
        }
        /// <summary>
        /// Check all MaxSpent plans and in they at deadline and accomplished set em as completed
        /// </summary>
        public void CheckAllMaxSpendDeadlines()
        {
            
            var accounts = _accountsQuery.Execute();
            foreach (var account in accounts) // FOR EACH ACCOUNT 
            {
                Query.Filter = new PlanModelFilter {AccountId = account.Id, PlanType = PlanTypeModel.MaxSpend, IsCompleted = false, DeadlineFrom = DateTime.Now};
                var maxSpendPlans = GetList();
                foreach (var plan in maxSpendPlans) // CHECK EVERY MAX SPEND PLAN THAT IS NO COMPLETED YET, REACHED ITS DEADLINE
                {
                    _costInfosQuery.Filter = new CostInfoModelFilter() // USES EVERY COST OF PLANNED TYPE FROM START TO DEADLINE
                    {
                        TypeId = plan.PlannedTypeId,
                        CreatedFrom = plan.Start,
                        CreatedTo = plan.Deadline
                    };
                    var costInfos = _costInfosQuery.Execute();
                    if (costInfos.Sum(x => x.Money) <= plan.PlannedMoney)
                    {
                        plan.IsCompleted = true;
                        Save(plan);
                    }
                }

            }
        }
    }
}
