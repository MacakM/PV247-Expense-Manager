using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.DataTransferObjects.Filters.AccountBadges;
using ExpenseManager.Business.DataTransferObjects.Filters.Accounts;
using ExpenseManager.Business.DataTransferObjects.Filters.Badges;
using ExpenseManager.Business.DataTransferObjects.Filters.CostInfos;
using ExpenseManager.Business.DataTransferObjects.Filters.CostTypes;
using ExpenseManager.Business.DataTransferObjects.Filters.Plans;
using ExpenseManager.Business.DataTransferObjects.Filters.Users;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Business.DataTransferObjects.Factories
{
    internal static class FilterFactory
    {
        internal static IPageAndOrderable<T> GetPageAndOrderable<T>(PageInfo pageInfo)
        {
            if (pageInfo == null)
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

        internal static IEnumerable<IFilter<CostInfoModel>> GetCostItemsFilters(Guid? accountId, Periodicity? periodicity, DateTime? dateFrom, DateTime? dateTo, decimal? moneyFrom, decimal? moneyTo, Guid? costTypeId, bool? isIncome)
        {
            yield return TryCreateFilter<CostInfosByAccountId, Guid>(accountId);
            yield return TryCreateFilter<CostInfosByPeriodicity, PeriodicityModel>((PeriodicityModel?)periodicity);
            yield return TryCreateFilter<CostInfosByCreatedFrom, DateTime>(dateFrom);
            yield return TryCreateFilter<CostInfosByCreatedTo, DateTime>(dateTo);
            yield return TryCreateFilter<CostInfosByMoneyFrom, decimal>(moneyFrom);
            yield return TryCreateFilter<CostInfosByMoneyTo, decimal>(moneyTo);
            yield return TryCreateFilter<CostInfosByTypeId, Guid>(costTypeId);
            yield return TryCreateFilter<CostInfosByIsIncome, bool>(isIncome);
        }

        internal static IEnumerable<IFilter<CostInfoModel>> GetCostItemsFilters(Guid? accountId, Periodicity? periodicity, DateTime? dateTo, bool? isIncome)
        {
            yield return TryCreateFilter<CostInfosByCreatedTo, DateTime>(dateTo);
            yield return TryCreateFilter<CostInfosByAccountId, Guid>(accountId);
            yield return TryCreateFilter<CostInfosByIsIncome, bool>(isIncome);
            yield return TryCreateFilter<CostInfosByPeriodicity, PeriodicityModel>((PeriodicityModel?)periodicity);
        }

        internal static IEnumerable<IFilter<CostInfoModel>> GetCostItemsFilters(Periodicity? periodicity)
        {
            yield return TryCreateFilter<CostInfosByPeriodicity, PeriodicityModel>((PeriodicityModel?)periodicity);
        }

        internal static IEnumerable<IFilter<CostInfoModel>> GetCostItemsFilters(DateTime? start, DateTime? end, Guid plannedTypeId)
        {
            yield return TryCreateFilter<CostInfosByCreatedFrom, DateTime>(start);
            yield return TryCreateFilter<CostInfosByCreatedTo, DateTime>(end);
            yield return TryCreateFilter<CostInfosByTypeId, Guid>(plannedTypeId);
        }
        internal static IEnumerable<IFilter<PlanModel>> GetPlanFilters(Guid? accountId, decimal? moneyTo, PlanType? planType, bool? completed, DateTime? dateTimeFrom)
        {
            yield return TryCreateFilter<PlansByAccountId, Guid>(accountId);
            yield return TryCreateFilter<PlansByMoneyTo, decimal>(moneyTo);
            yield return TryCreateFilter<PlansByType, PlanTypeModel>((PlanTypeModel?)planType);
            yield return TryCreateFilter<PlansByCompletition, bool>(completed);
            yield return TryCreateFilter<PlansByDeadlineFrom, DateTime>(dateTimeFrom);
        }
        internal static IEnumerable<IFilter<PlanModel>> GetPlanFilters(Guid? accountId)
        {
            yield return TryCreateFilter<PlansByAccountId, Guid>(accountId);
        }

        internal static IEnumerable<IFilter<PlanModel>> GetPlanFilters(Guid? accountId, decimal? moneyTo, PlanType? planType, bool? completed)
        {
            yield return TryCreateFilter<PlansByAccountId, Guid>(accountId);
            yield return TryCreateFilter<PlansByMoneyTo, decimal>(moneyTo);
            yield return TryCreateFilter<PlansByType, PlanTypeModel>((PlanTypeModel?)planType);
            yield return TryCreateFilter<PlansByCompletition, bool>(completed);
        }

        internal static IEnumerable<IFilter<PlanModel>> GetPlanFilters(Guid? accountId, bool? completed, DateTime dateTimeFrom)
        {
            yield return TryCreateFilter<PlansByAccountId, Guid>(accountId);
            yield return TryCreateFilter<PlansByCompletition, bool>(completed);
            yield return TryCreateFilter<PlansByDeadlineFrom, DateTime>(dateTimeFrom);
        }

        internal static IEnumerable<IFilter<CostTypeModel>> GetCostTypeFilters(string costTypeName, Guid? accountId)
        {
            yield return TryCreateFilter<CostTypesByName, string>(costTypeName);
            yield return TryCreateFilter<CostTypesByAccountId, Guid>(accountId);
        }

        internal static IEnumerable<IFilter<CostTypeModel>> GetCostTypeFilters(Guid? accountId)
        {
            yield return TryCreateFilter<CostTypesByAccountId, Guid>(accountId);
        }

        internal static IEnumerable<IFilter<BadgeModel>> GetBadgeFilters(string badgeName)
        {
            yield return TryCreateFilter<BadgesByName, string>(badgeName);
        }

        internal static IEnumerable<IFilter<AccountBadgeModel>> GetAccountBadgeFilters(Guid? accountId)
        {
            yield return TryCreateFilter<AccountBadgesByAccountId, Guid>(accountId);
        }

        internal static IEnumerable<IFilter<UserModel>> GetUserFilters(Guid? accountId, AccountAccessType? accessType,
            string email)
        {
            yield return TryCreateFilter<UsersByAccountId, Guid>(accountId);
            yield return TryCreateFilter<UsersByAccessType, AccountAccessTypeModel>((AccountAccessTypeModel?)accessType);
            yield return TryCreateFilter<UsersByEmail, string>(email);
        }

        internal static IEnumerable<IFilter<AccountModel>> GetAccountFilters(string accountName)
        {
            yield return TryCreateFilter<AccountsByName, string>(accountName);
        }

        private static TFilter TryCreateFilter<TFilter, TValue>(TValue value)
            where TFilter : class, IFilterValue<TValue>, new()
        {
            return value != null ? new TFilter() { Value = value } : null;
        }
        private static TFilter TryCreateFilter<TFilter, TValue>(TValue? value)
            where TFilter : class, IFilterValue<TValue>, new()
            where TValue : struct
        {
            return value.HasValue ? new TFilter { Value = value.Value } : null;
        }


    }
}
