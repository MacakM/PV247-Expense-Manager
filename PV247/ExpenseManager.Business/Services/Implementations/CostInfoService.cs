using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.DataTransferObjects.Filters.CostInfos;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;
using ExpenseManager.Database.Infrastructure.Query;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// Service handles AccountBadge entity operations
    /// </summary>
    internal class CostInfoService : ExpenseManagerQueryAndCrudServiceBase<CostInfoModel, Guid, CostInfo>, ICostInfoService
    {
        private readonly ExpenseManagerRepository<CostTypeModel, Guid> _costTypeRepository;

        private readonly ExpenseManagerRepository<AccountModel, Guid> _accountRepository;

        /// <summary>
        /// Incluede entities
        /// </summary>
        protected override string[] EntityIncludes { get; } =
        {
            nameof(CostInfoModel.Account),
            nameof(CostInfoModel.Type)
        };

        /// <summary>
        /// Cost infore service constructor
        /// </summary>
        /// <param name="query">Query</param>
        /// <param name="repository">Repository</param>
        /// <param name="expenseManagerMapper">Expense manager mapper</param>
        /// <param name="unitOfWorkProvider">Unit of work provider</param>
        /// <param name="costTypeRepository">Cost type repository</param>
        /// <param name="accountRepository">Account repository</param>
        internal CostInfoService(
            ExpenseManagerQuery<CostInfoModel> query,
            ExpenseManagerRepository<CostInfoModel, Guid> repository,
            Mapper expenseManagerMapper,
            IUnitOfWorkProvider unitOfWorkProvider,
            ExpenseManagerRepository<CostTypeModel, Guid> costTypeRepository,
            ExpenseManagerRepository<AccountModel, Guid> accountRepository) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
            _costTypeRepository = costTypeRepository;
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// Creates new cost info object in databse
        /// </summary>
        /// <param name="costInfo"></param>
        public Guid CreateCostInfo(CostInfo costInfo)
        {
            var costInfoModel = ExpenseManagerMapper.Map<CostInfoModel>(costInfo);
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                var account = _accountRepository.GetById(costInfo.AccountId);
                var type = _costTypeRepository.GetById(costInfo.TypeId);

                if (account == null || type == null)
                {
                    throw new InvalidOperationException("Account of type doesn't exists");
                }

                if (type.AccountId != account.Id)
                {
                    throw new InvalidOperationException("Cost type doesn't belong to given account");
                }

                costInfoModel.Account = account;
                costInfoModel.Type = type;

                Repository.Insert(costInfoModel);
                unitOfWork.Commit();
            }
            return costInfoModel.Id;
        }

        /// <summary>
        /// Updates existing cost info
        /// </summary>
        /// <param name="updatedCostInfo">updated cost info</param>
        public void UpdateCostInfo(CostInfo updatedCostInfo)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Save(updatedCostInfo);
                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Deletes cost info specified by cost info
        /// </summary>
        /// <param name="costInfoId"></param>
        public void DeleteCostInfo(Guid costInfoId)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Delete(costInfoId);
                unitOfWork.Commit();
            }           
        }

        /// <summary>
        /// Get cost info specified by unique id
        /// </summary>
        /// <param name="costInfoId">Unique id</param>
        /// <returns>Cost info</returns>
        public CostInfo GetCostInfo(Guid costInfoId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return GetDetail(costInfoId);
            }           
        }     

        /// <summary>
        /// List cost types based on filter
        /// </summary>
        /// <param name="filters">Filters cost infos</param>
        /// <param name="pageAndOrder"></param>
        /// <returns>List of cost infos</returns>
        public List<CostInfo> ListCostInfos(List<IFilter<CostInfoModel>> filters, IPageAndOrderable<CostInfoModel> pageAndOrder)
        {
            Query.Filters = filters;
            Query.PageAndOrderModelFilterModel = pageAndOrder;
            using (UnitOfWorkProvider.Create())
            {
                return GetList().ToList();
            }          
        }
     

        /// <summary>
        /// Gets the count of rows in database filtered by filter
        /// Used for pagination
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="pageAndOrder"></param>
        /// <returns></returns>
        public int GetCostInfosCount(List<IFilter<CostInfoModel>> filters, IPageAndOrderable<CostInfoModel> pageAndOrder)
        {
            Query.Filters = filters;
            Query.PageAndOrderModelFilterModel = pageAndOrder;
            using (UnitOfWorkProvider.Create())
            {
                return Query.GetTotalRowCount();
            }
        }

        /// <summary>
        /// Recompute periodic costs and make them as new cost infos
        /// List periodic updates. Check if its right time to set it as cost, and clone it as non periodic ones.
        /// Also set periodic cost info created time to DateTime.Now in order to start new period.
        /// </summary>
        public void RecomputePeriodicCosts()
        {
            CheckDayPeriodicites();
            CheckWeekPeriodicities();
            CheckMonthPeriodicities();
        }
        

        /// <summary>
        /// Returns current balance of account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public decimal GetBalance(Guid accountId)
        {
            IList<CostInfo> incomes;
            IList<CostInfo> outcomes;
            using (UnitOfWorkProvider.Create())
            {
                Query.Filters = new List<IFilter<CostInfoModel>>
                {
                    new CostInfosByIsIncome { IsIncome = true},
                    new CostInfosByPeriodicity {Periodicity = PeriodicityModel.None },
                    new CostInfosByCreatedTo { CreatedTo = DateTime.Now },
                    new CostInfosByAccountId {AccountId = accountId}
                }; 
                
                incomes = GetList();
                Query.Filters = new List<IFilter<CostInfoModel>>
                {
                   new CostInfosByIsIncome { IsIncome = false},
                    new CostInfosByPeriodicity {Periodicity = PeriodicityModel.None },
                    new CostInfosByCreatedTo { CreatedTo = DateTime.Now },
                    new CostInfosByAccountId {AccountId = accountId}
                };
                
                outcomes = GetList();
            }

            return incomes.Sum(x =>  x.Money) - outcomes.Sum(x => x.Money);
        }

        private void CheckMonthPeriodicities()
        {

            CostInfosByPeriodicity filter = new CostInfosByPeriodicity { Periodicity = PeriodicityModel.Month};

            Query.Filters = new List<IFilter<CostInfoModel>> { filter };

            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                var monthPeriodicityCosts = GetList().ToList();

                foreach (var monthPeriodicity in monthPeriodicityCosts)
                {
                    if (monthPeriodicity.Created != null)
                    {
                        var created = monthPeriodicity.Created.Value;
                        if (created <= DateTime.Today) // I look if today is later then next creating time
                        {
                            Save(CloneAsNonPeriodic(monthPeriodicity));
                            monthPeriodicity.Created = DateTime.Now.AddMonths(monthPeriodicity.PeriodicMultiplicity);
                            Save(monthPeriodicity);
                        }
                    }
                }
                unitOfWork.Commit();
            }
            
        }

        private void CheckWeekPeriodicities()
        {

            CostInfosByPeriodicity filter = new CostInfosByPeriodicity { Periodicity = PeriodicityModel.Week };

            Query.Filters = new List<IFilter<CostInfoModel>> { filter };
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                var dayPeriodicityCosts = GetList().ToList();

                foreach (var weekPeriodicityCost in dayPeriodicityCosts)
                {
                    if (weekPeriodicityCost.Created != null)
                    {
                        var created = weekPeriodicityCost.Created.Value;
                        if (created <= DateTime.Today) // Week usually has 7 days  // I look if today is later then next creating time
                        {
                            Save(CloneAsNonPeriodic(weekPeriodicityCost));
                            weekPeriodicityCost.Created = DateTime.Now.AddDays(7 * weekPeriodicityCost.PeriodicMultiplicity);
                            Save(weekPeriodicityCost);
                        }
                    }
                }
                unitOfWork.Commit();
            }
        }

        private void CheckDayPeriodicites()
        {
            CostInfosByPeriodicity filter = new CostInfosByPeriodicity { Periodicity = PeriodicityModel.Day};

            Query.Filters = new List<IFilter<CostInfoModel>> { filter};
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                var dayPeriodicityCosts = GetList().ToList();

                foreach (var dayPeriodicityCost in dayPeriodicityCosts)
                {
                    if (dayPeriodicityCost.Created != null)
                    {
                        var created = dayPeriodicityCost.Created.Value;
                        if (created <= DateTime.Today)  // I look if today is later then next creating time
                        {
                            Save(CloneAsNonPeriodic(dayPeriodicityCost));
                            dayPeriodicityCost.Created = DateTime.Now.AddDays(dayPeriodicityCost.PeriodicMultiplicity);
                            Save(dayPeriodicityCost);
                        }
                    }
                }
                unitOfWork.Commit();
            }
        }

        private CostInfo CloneAsNonPeriodic(CostInfo periodic)
        {
            CostInfo nonPeriodic = new CostInfo
            {
                Created = DateTime.Now,
                PeriodicMultiplicity = 0,
                AccountId = periodic.AccountId,
                AccountName = periodic.AccountName,
                Description = periodic.Description,
                IsIncome = periodic.IsIncome,
                Money = periodic.Money,
                Periodicity = Periodicity.None,
                TypeId = periodic.TypeId,
                TypeName = periodic.TypeName
            };
            
            return nonPeriodic;
        }
    }
}
