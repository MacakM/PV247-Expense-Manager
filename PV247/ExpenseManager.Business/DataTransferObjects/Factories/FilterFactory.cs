using System;
using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.DataTransferObjects.Filters.Accounts;
using ExpenseManager.Business.DataTransferObjects.Filters.Badges;
using ExpenseManager.Business.DataTransferObjects.Filters.CostInfos;
using ExpenseManager.Business.DataTransferObjects.Filters.CostTypes;
using ExpenseManager.Business.DataTransferObjects.Filters.Plans;
using ExpenseManager.Business.DataTransferObjects.Filters.Users;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Factories
{
    internal static class FilterFactory 
    {
        internal static IPageAndOrderable<T> GetPageAndOrderable<T>(PageInfo pageInfo)
        {
            if(pageInfo == null)
            {
                return null;
            }
            return new PageAndOrderFilter<T>
            {
                OrderByDesc = pageInfo.OrderByDesc,
                OrderByPropertyName = pageInfo.OrderByPropertyName,
                PageNumber = pageInfo.PageNumber,
                PageSize = pageInfo.PageSize
            };
        }

        public static List<IFilter<CostInfoModel>> GetCostItemsFilters(Guid? accountId, Periodicity? periodicity, DateTime? dateFrom, DateTime? dateTo, decimal? moneyFrom, decimal? moneyTo, Guid? costTypeId, bool? isIncome)
        {
            return new List<IFilter<CostInfoModel>>
            {
                new CostInfosByAccountId(accountId),
                new CostInfosByPeriodicity(periodicity),
                new CostInfosByCreatedFrom(dateFrom),
                new CostInfosByCreatedTo(dateTo),
                new CostInfosByMoneyFrom(moneyFrom),
                new CostInfosByMoneyTo(moneyTo),
                new CostInfosByTypeId(costTypeId),
                new CostInfosByIsIncome(isIncome)
            };
        }

        public static List<IFilter<PlanModel>> GetPlanFilters(Guid? accountId)
        {
           return new List<IFilter<PlanModel>> { new PlansByAccountId(accountId)};
        }

        public static List<IFilter<CostTypeModel>> GetCostTypeFilters(string costTypeName, Guid accountId)
        {
            return new List<IFilter<CostTypeModel>>
            {
                new CostTypesByName(costTypeName),
                new CostTypesByAccountId(accountId)
            };
        }

        public static List<IFilter<CostTypeModel>> GetCostTypeFilters(Guid accountId)
        {
            return new List<IFilter<CostTypeModel>> { new CostTypesByAccountId(accountId)};
        }

        public static List<IFilter<BadgeModel>> GetBadgeFilters(string badgeName)
        {
            return new List<IFilter<BadgeModel>> { new BadgesByName(badgeName)};
        }

        public static List<IFilter<AccountBadgeModel>> GetAccountBadgeFilters(Guid accountId)
        {
            return new  List<IFilter<AccountBadgeModel>> { new AccountBadgesByAccountId(accountId)};
        }

        public static List<IFilter<UserModel>> GetUserFilters(Guid? accountId, AccountAccessType? accessType,
            string email)
        {
            return new List<IFilter<UserModel>> {new UsersByAccountId(accountId), new UsersByAccessType(accessType), new UsersByEmail(email)};
        }

        public static List<IFilter<AccountModel>> GetAccountFilters(string accountName)
        {
            return new List<IFilter<AccountModel>> { new AccountsByName(accountName)};

        }
    }
}
