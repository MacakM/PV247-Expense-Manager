using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
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
    /// Service handles AccountBadge entity operations
    /// </summary>
    public class CostInfoService : ExpenseManagerQueryAndCrudServiceBase<CostInfoModel, int, CostInfo, CostInfoModelFilter>, ICostInfoService
    {
        private readonly CostTypeRepository _costTypeRepository;
        private readonly AccountRepository _accountRepository;

        /// <summary>
        /// 
        /// </summary>
        protected override string[] EntityIncludes { get; } =
        {
            nameof(CostInfoModel.Account),
            nameof(CostInfoModel.Type)
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="repository"></param>
        /// <param name="expenseManagerMapper"></param>
        /// <param name="unitOfWorkProvider"></param>
        /// <param name="costTypeRepository"></param>
        /// <param name="accountRepository"></param>
        public CostInfoService(
            ExpenseManagerQuery<CostInfoModel, CostInfoModelFilter> query, 
            ExpenseManagerRepository<CostInfoModel, int> repository, 
            Mapper expenseManagerMapper, 
            IUnitOfWorkProvider unitOfWorkProvider,
            CostTypeRepository costTypeRepository,
            AccountRepository accountRepository) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
            _costTypeRepository = costTypeRepository;
            _accountRepository = accountRepository;
        }
        /// <summary>
        /// Creates new cost info object in databse
        /// </summary>
        /// <param name="costInfo"></param>
        public void CreateCostInfo(CostInfo costInfo)
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
        public void DeleteCostInfo(int costInfoId)
        {
            Delete(costInfoId);
        }
        /// <summary>
        /// Get cost info specified by unique id
        /// </summary>
        /// <param name="costInfoId">Unique id</param>
        /// <returns>Cost info</returns>
        public CostInfo GetCostInfo(int costInfoId)
        {
            return GetDetail(costInfoId);
        }
        /// <summary>
        /// List cost types based on filter
        /// </summary>
        /// <param name="filter">Filters cost infos</param>
        /// <returns>List of cost infos</returns>
        public List<CostInfo> ListCostInfos(CostInfoFilter filter)
        {
            Query.Filter = ExpenseManagerMapper.Map<CostInfoModelFilter>(filter);
            Query.Filter.OrderByDesc = true;
            Query.Filter.OrderByPropertyName = nameof(CostInfo.Created);
            return GetList().ToList();
        }

        /// <summary>
        /// Gets the count of rows in database filtered by filter
        /// Used for pagination
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int GetCostInfosCount(CostInfoFilter filter)
        {
            Query.Filter = ExpenseManagerMapper.Map<CostInfoModelFilter>(filter);
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
                    if (created.AddMonths(monthPeriodicity.PeriodicMultiplicity) >= DateTime.Today)
                    {
                        monthPeriodicity.Created = DateTime.Now;
                        Save(monthPeriodicity);
                        Save(CloneAsNonPeriodic(monthPeriodicity));
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
                    if (created.AddDays(7*weekPeriodicityCost.PeriodicMultiplicity) >= DateTime.Today) // Week usually has 7 days
                    {
                        weekPeriodicityCost.Created = DateTime.Now;
                        Save(weekPeriodicityCost);
                        Save(CloneAsNonPeriodic(weekPeriodicityCost));
                    }
                }
            }
        }

        private void CheckDayPeriodicites()
        {
            CostInfoFilter filter = new CostInfoFilter {Periodicity = Periodicity.Day};

            Query.Filter = ExpenseManagerMapper.Map<CostInfoModelFilter>(filter);
            var dayPeriodicityCosts = GetList().ToList();

            foreach (var dayPeriodicityCost in dayPeriodicityCosts)
            {
                if (dayPeriodicityCost.Created != null)
                {
                    var created = dayPeriodicityCost.Created.Value;
                    if (created.AddDays(dayPeriodicityCost.PeriodicMultiplicity) >= DateTime.Today)
                    {
                        dayPeriodicityCost.Created = DateTime.Now;
                        Save(dayPeriodicityCost);
                        Save(CloneAsNonPeriodic(dayPeriodicityCost));
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
