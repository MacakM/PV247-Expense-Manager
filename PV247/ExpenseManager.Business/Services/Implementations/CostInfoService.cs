using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// Service handles AccountBadge entity operations
    /// </summary>
    public class CostInfoService : ExpenseManagerQueryAndCrudServiceBase<CostInfoModel, Guid, CostInfo>, ICostInfoService
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
        public CostInfoService(
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
            using (var uow = UnitOfWorkProvider.Create())
            {
                var account = _accountRepository.GetById(costInfo.AccountId);
                var type = _costTypeRepository.GetById(costInfo.TypeId);

                if (account == null || type == null)
                {
                    throw new InvalidOperationException("Account of type doesn't exists");
                }

                costInfoModel.Account = account;
                costInfoModel.Type = type;

                Repository.Insert(costInfoModel);
                uow.Commit();
            }
            return costInfoModel.Id;
        }

        /// <summary>
        /// Updates existing cost info
        /// </summary>
        /// <param name="updatedCostInfo">updated cost info</param>
        public void UpdateCostInfo(CostInfo updatedCostInfo)
        {
            Save(updatedCostInfo);
        }

        /// <summary>
        /// Deletes cost info specified by cost info
        /// </summary>
        /// <param name="costInfoId"></param>
        public void DeleteCostInfo(Guid costInfoId)
        {
            Delete(costInfoId);
        }

        /// <summary>
        /// Get cost info specified by unique id
        /// </summary>
        /// <param name="costInfoId">Unique id</param>
        /// <returns>Cost info</returns>
        public CostInfo GetCostInfo(Guid costInfoId)
        {
            return GetDetail(costInfoId);
        }

        /// <summary>
        /// List cost types based on filter
        /// </summary>
        /// <param name="filters">Filters cost infos</param>
        /// <param name="pageAndOrder"></param>
        /// <returns>List of cost infos</returns>
        public List<CostInfo> ListCostInfos(List<IFilter<CostInfo>> filters, PageAndOrderFilter pageAndOrder)
        {
            Query.Filters = ExpenseManagerMapper.Map<List<IFilterModel<CostInfoModel>>>(filters);
            Query.PageAndOrderModelFilterModel =
                ExpenseManagerMapper.Map<PageAndOrderModelFilterModel<CostInfoModel>>(pageAndOrder);
            return GetList().ToList();
        }

        /// <summary>
        /// Gets the count of rows in database filtered by filter
        /// Used for pagination
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="pageAndOrder"></param>
        /// <returns></returns>
        public int GetCostInfosCount(List<IFilter<CostInfo>> filters, PageAndOrderFilter pageAndOrder)
        {
            Query.Filters = ExpenseManagerMapper.Map<List<IFilterModel<CostInfoModel>>>(filters);
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
            Query.Filters = new CostInfoModelFilter { IsIncome = true, Periodicity = PeriodicityModel.None, CreatedTo = DateTime.Now, AccountId = accountId};
            var incomes = GetList();
            Query.Filters = new CostInfoModelFilter { IsIncome = false, Periodicity = PeriodicityModel.None, CreatedTo = DateTime.Now, AccountId = accountId };
            var outcomes = GetList();

            return incomes.Sum(x =>  x.Money) - outcomes.Sum(x => x.Money);
        }

        private void CheckMonthPeriodicities()
        {
            CostInfoFilter filter = new CostInfoFilter { Periodicity = Periodicity.Month };

            Query.Filter = ExpenseManagerMapper.Map<CostInfoModelFilter>(filter);
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
        }

        private void CheckWeekPeriodicities()
        {
            CostInfoFilter filter = new CostInfoFilter { Periodicity = Periodicity.Week };

            Query.Filter = ExpenseManagerMapper.Map<CostInfoModelFilter>(filter);
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
        }

        private void CheckDayPeriodicites()
        {
            CostInfoFilter filter = new CostInfoFilter { Periodicity = Periodicity.Day };

            Query.Filter = ExpenseManagerMapper.Map<CostInfoModelFilter>(filter);
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
