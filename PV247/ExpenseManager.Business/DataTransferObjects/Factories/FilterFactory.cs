using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        internal static List<IFilter<T>> GetFilters<T>(params Tuple<string, object>[] values)
        {
            var notNullValues = values.Where(x => x.Item2 != null).ToList();
            var output = new List<IFilter<T>>();
            var filters = Assembly.GetAssembly(typeof (FilterFactory)).GetTypes().Where(t => !t.IsInterface && t.GetInterfaces().Any(i => i.Name == typeof(IFilter<>).Name && i.GenericTypeArguments[0] == typeof(T)) && MutualProperties(t, notNullValues).Any());

            foreach (var filter in filters)
            {
                var instance =(IFilter<T>)Activator.CreateInstance(filter);
                foreach (var value in MutualProperties(filter, notNullValues.ToList()))
                {
                    var property = filter.GetProperty(value.Item1, BindingFlags.Public | BindingFlags.Instance);
                    if (null != property && property.CanWrite)
                    {
                        property.SetValue(instance, value.Item2);
                    }
                }
                output.Add(instance);
            }
            return output;
        }
        
        private static IEnumerable<Tuple<string, object>> MutualProperties(Type type, List<Tuple<string, object>> values)
        {
            return values.Where(x => type.GetProperties().Select(y=>y.Name).Contains(x.Item1));
        }

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
            return GetFilters<CostInfoModel>(
                new Tuple<string, object>(nameof(CostInfosByAccountId.AccountId), accountId),
                new Tuple<string, object>(nameof(CostInfosByPeriodicity.Periodicity), periodicity),
                new Tuple<string, object>(nameof(CostInfosByCreatedFrom.CreatedFrom), dateFrom),
                new Tuple<string, object>(nameof(CostInfosByCreatedTo.CreatedTo), dateTo),
                new Tuple<string, object>(nameof(CostInfosByMoneyFrom.MoneyFrom), moneyFrom),
                new Tuple<string, object>(nameof(CostInfosByMoneyTo.MoneyTo), moneyTo),
                new Tuple<string, object>(nameof(CostInfosByTypeId.TypeId), costTypeId),
                new Tuple<string, object>(nameof(CostInfosByIsIncome.IsIncome), isIncome));
        }

        public static List<IFilter<PlanModel>> GetPlanFilters(Guid? accountId)
        {
           return GetFilters<PlanModel>(new Tuple<string, object>(nameof(PlansByAccountId.AccountId),
                accountId));
        }

        public static List<IFilter<CostTypeModel>> GetCostTypeFilters(string costTypeName)
        {
            return GetFilters<CostTypeModel>(new Tuple<string, object>(nameof(CostTypesByName.Name),
                    costTypeName));
        }

        public static List<IFilter<CostTypeModel>> GetCostTypeFilters(Guid accountId)
        {
            return GetFilters<CostTypeModel>(new Tuple<string, object>(nameof(CostTypesByAccountId.AccountId),
                 accountId));
        }


        public static List<IFilter<BadgeModel>> GetBadgeFilters(string badgeName)
        {
            return GetFilters<BadgeModel>(new Tuple<string, object>(nameof(BadgesByName.Name), badgeName));
        }

        public static List<IFilter<AccountBadgeModel>> GetAccountBadgeFilters(Guid accountId)
        {
            return GetFilters<AccountBadgeModel>(new Tuple<string, object>(nameof(AccountBadgesByAccountId.AccountId), accountId));
        }

        public static List<IFilter<UserModel>> GetUserFilters(Guid? accountId, AccountAccessType? accessType, string email)
        {
            return GetFilters<UserModel>(new Tuple<string, object>(nameof(UsersByAccountId.AccountId), accountId), new Tuple<string, object>(nameof(UsersByAccessType.AccessType), accessType), new Tuple<string, object>(nameof(UsersByEmail.Email), email));
        }

        public static List<IFilter<AccountModel>> GetAccountFilters(string accountName)
        {
            return GetFilters<AccountModel>(new Tuple<string, object>(nameof(AccountsByName.Name), accountName));

        }
    }
}
