using System;
using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    public class ListCostInfosQuery : ExpenseManagerQuery<CostInfoModel, CostInfoModelFilter>
    {
        public ListCostInfosQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        public override CostInfoModelFilter Filter { get; set; }

        protected override IQueryable<CostInfoModel> GetQueryable()
        {
            IQueryable<CostInfoModel> costInfos = Context.CostInfos.Include(nameof(CostInfoModel.Account)).Include(nameof(CostInfoModel.Type));

            if (Filter == null)
            {
                return costInfos;
            }
            if (!string.IsNullOrEmpty(Filter.AccountName))
            {
                costInfos = Filter.DoExactMatch ? costInfos.Where(costInfo => costInfo.Account.Name.Equals(Filter.AccountName)) : costInfos.Where(costInfo => costInfo.Account.Name.Contains(Filter.AccountName));
            }
            if (!string.IsNullOrEmpty(Filter.TypeName))
            {
                costInfos = Filter.DoExactMatch ? costInfos.Where(costInfo => costInfo.Type.Name.Equals(Filter.TypeName)) : costInfos.Where(costInfo => costInfo.Type.Name.Contains(Filter.TypeName));
            }
            if (Filter.AccountId != null)
            {
                costInfos = costInfos.Where(costInfo => costInfo.AccountId == Filter.AccountId.Value);
            }
            if (Filter.IsIncome != null)
            {
                costInfos = costInfos.Where(costInfo => costInfo.IsIncome == Filter.IsIncome.Value);
            }
            if (Filter.IsPeriodic != null)
            {
                costInfos = costInfos.Where(costInfo => costInfo.IsPeriodic == Filter.IsPeriodic.Value);
            }
            if (Filter.TypeId != null)
            {
                costInfos = costInfos.Where(costInfo => costInfo.TypeId == Filter.TypeId.Value);
            }
            if (Filter.CreatedFrom != null)
            {
                costInfos = costInfos.Where(costInfo => costInfo.Created >= Filter.CreatedFrom.Value);
            }
            if (Filter.CreatedTo != null)
            {
                costInfos = costInfos.Where(costInfo => costInfo.Created <= Filter.CreatedTo.Value);
            }
            if (Filter.MoneyFrom != null)
            {
                costInfos = costInfos.Where(costInfo => costInfo.Money >= Filter.MoneyFrom.Value);
            }
            if (Filter.MoneyTo != null)
            {
                costInfos = costInfos.Where(costInfo => costInfo.Money <= Filter.MoneyTo.Value);
            }
            if (Filter.OrderByDesc == null || string.IsNullOrEmpty(Filter.OrderByPropertyName))
            {
                return costInfos;
            }
            System.Reflection.PropertyInfo prop = typeof(AccountBadgeModel).GetProperty(Filter.OrderByPropertyName);
            if (prop == null)
            {
                return costInfos.Take(Filter.PageSize);
            }
            costInfos = Filter.OrderByDesc.Value ? costInfos.OrderByDescending(x => prop.GetValue(x, null)) : costInfos.OrderBy(x => prop.GetValue(x, null));
            if (Filter.PageNumber != null)
            {
                costInfos = costInfos.Skip(Math.Max(0, Filter.PageNumber.Value - 1) * Filter.PageSize);
            }
            return costInfos.Take(Filter.PageSize);
        }
    }
}
