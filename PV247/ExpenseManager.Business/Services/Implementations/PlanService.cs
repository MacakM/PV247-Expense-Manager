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
using ExpenseManager.Database.Filters.CostInfos;
using ExpenseManager.Database.Filters.Plans;
using ExpenseManager.Database.Infrastructure.Query;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// Service handles plan entity operations
    /// </summary>
    public class PlanService : ExpenseManagerQueryAndCrudServiceBase<PlanModel, Guid, Plan>, IPlanService
    {
        private readonly CostInfoRepository _costInfoRepository;

        private readonly ListAccountsQuery _accountsQuery;

        private readonly ListCostInfosQuery _costInfosQuery;

        private readonly ExpenseManagerRepository<CostTypeModel, Guid> _costTypeRepository;

        private readonly ExpenseManagerRepository<AccountModel, Guid> _accountRepository;

        /// <summary>
        /// Entity includes
        /// </summary>
        protected override string[] EntityIncludes { get; } =
        {
            nameof(PlanModel.Account),
            nameof(PlanModel.PlannedType)
        };

        /// <summary>
        /// Plan service constructor
        /// </summary>
        /// <param name="query">Query</param>
        /// <param name="repository">Repository</param>
        /// <param name="expenseManagerMapper">Mapper</param>
        /// <param name="unitOfWorkProvider">Unit of work provider</param>
        /// <param name="costInfoRepository">Repository</param>
        /// <param name="accountsQuery">Accounts query</param>
        /// <param name="costInfosQuery">Cost info query</param>
        /// <param name="costTypeRepository">Cost type repository</param>
        /// <param name="accountRepository">Account repository</param>
        public PlanService(ExpenseManagerQuery<PlanModel> query, 
            ExpenseManagerRepository<PlanModel, Guid> repository,
            Mapper expenseManagerMapper, 
            IUnitOfWorkProvider unitOfWorkProvider, 
            CostInfoRepository costInfoRepository, 
            ListAccountsQuery accountsQuery, 
            ListCostInfosQuery costInfosQuery,
            ExpenseManagerRepository<CostTypeModel, Guid> costTypeRepository,
            ExpenseManagerRepository<AccountModel, Guid> accountRepository) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
            _costInfoRepository = costInfoRepository;
            _accountsQuery = accountsQuery;
            _costInfosQuery = costInfosQuery;
            _costTypeRepository = costTypeRepository;
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// Creates new plan in databse
        /// </summary>
        /// <param name="plan">Object to be saved to database</param>
        public Guid CreatePlan(Plan plan)
        {
            var planModel = ExpenseManagerMapper.Map<PlanModel>(plan);
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (plan.AccountId != null)
                {
                    var account = _accountRepository.GetById(plan.AccountId.Value);
                    var type = _costTypeRepository.GetById(plan.PlannedTypeId);

                    if (account == null || type == null)
                    {
                        throw new InvalidOperationException("Account of type doesn't exists");
                    }

                    planModel.Account = account;
                    planModel.PlannedType = type;
                }

                Repository.Insert(planModel);
                uow.Commit();
            }
            return planModel.Id;
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
        public void DeletePlan(Guid planId)
        {
            Delete(planId);
        }

        /// <summary>
        /// Get specific plan specified by unique id
        /// </summary>
        /// <param name="planId">Unique id of plan</param>
        /// <returns></returns>
        public Plan GetPlan(Guid planId)
        {
            return GetDetail(planId);
        }

        /// <summary>
        /// Lists all plans that match filters criterias
        /// </summary>
        /// <param name="filters">Filters plans</param>
        /// <param name="pageAndOrder">Orders</param>
        /// <returns></returns>
        public List<Plan> ListPlans(List<IFilter<Plan>> filters, PageAndOrderFilter pageAndOrder)
        {
            Query.Filters = ExpenseManagerMapper.Map<List<IFilterModel<PlanModel>>>(filters);
            Query.PageAndOrderModelFilterModel =
                ExpenseManagerMapper.Map<PageAndOrderModelFilterModel<PlanModel>>(pageAndOrder);
            Query.AddSortCriteria(x => x.Start, SortDirection.Descending);
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
               throw new ArgumentException("PlanType.Save is the right one.");
            }

            using (var uow = UnitOfWorkProvider.Create())
            {
                var planModel = Repository.GetById(plan.Id, EntityIncludes);
                if (planModel == null)
                {
                    throw new InvalidOperationException("Plan with given id doesn't exist");
                }

                planModel.IsCompleted = true;
                CloneToCost(planModel);
                uow.Commit();
            }
        }

        private void CloneToCost(PlanModel plan)
        {
            CostInfoModel costInfo = new CostInfoModel
            {
                Money = plan.PlannedMoney,
                Account = plan.Account,
                Type = plan.PlannedType,
                Created = DateTime.Now,
                IsIncome = false,
                Description = plan.Description,
                PeriodicMultiplicity = 0,
                Periodicity = PeriodicityModel.None
            };

            _costInfoRepository.Insert(costInfo);
        }

        /// <summary>
        /// Lists all plans, that can be closed by user
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="accountBalance"></param>
        /// <returns></returns>
        public List<Plan> ListAllCloseablePlans(Guid accountId, decimal accountBalance)
        {

            Query.Filters = new List<IFilterModel<PlanModel>>
            {
                new PlanModelsByAccountId(accountId),
                new PlanModelsByPlannedMoneyTo(accountBalance),
                new PlanModelsByPlanType(PlanTypeModel.Save),
                new PlanModelsByCompletition(false),
                new PlanModelsByDeadlineFrom(DateTime.Now)
            };
            return GetList().ToList();
        }

        /// <inheritdoc />
        public List<Plan> ListPlansInProgress(Guid accountId)
        {
            Query.Filters = new List<IFilterModel<PlanModel>>
            {
                new PlanModelsByAccountId(accountId),
                new PlanModelsByCompletition(false),
                new PlanModelsByDeadlineFrom(DateTime.Now)
            };
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
                Query.Filters = new  List<IFilterModel<PlanModel>>
                {
                       new PlanModelsByAccountId(account.Id),
                        new PlanModelsByPlanType(PlanTypeModel.MaxSpend),
                     new PlanModelsByCompletition(false),
                        new PlanModelsByDeadlineFrom(DateTime.Now)
                };
                var maxSpendPlans = GetList();
                foreach (var plan in maxSpendPlans) // CHECK EVERY MAX SPEND PLAN THAT IS NO COMPLETED YET, REACHED ITS DEADLINE
                {
                    _costInfosQuery.Filters = new List<IFilterModel<CostInfoModel>> // USES EVERY COST OF PLANNED TYPE FROM START TO DEADLINE
                    {
                        new CostInfoModelsByPlannedTypeId(plan.PlannedTypeId),
                        new CostInfoModelsByCreatedFrom(plan.Start),
                        new CostInfoModelsByCreatedTo(plan.Deadline)
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
