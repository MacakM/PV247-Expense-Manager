using System;
using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for cost infos.
    /// </summary>
    public class ListCostInfosQuery : ExpenseManagerQuery<CostInfoModel, CostInfoModelFilter>
    {
        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public ListCostInfosQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }
        /// <summary>
        /// Cost info filter
        /// </summary>
        public override CostInfoModelFilter Filter { get; set; }
        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
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
            if (Filter.Periodicity != null)
            {
                costInfos = costInfos.Where(costInfo => costInfo.Periodicity == Filter.Periodicity.Value);
            }
            if (Filter.PeriodicMultiplicityFrom != null)
            {
                costInfos =
                    costInfos.Where(
                        costInfo => costInfo.PeriodicMultiplicity.Value >= Filter.PeriodicMultiplicityFrom.Value);
            }
            if (Filter.PeriodicMultiplicityTo != null)
            {
                costInfos =
                    costInfos.Where(
                        costInfo => costInfo.PeriodicMultiplicity.Value <= Filter.PeriodicMultiplicityTo.Value);
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
            System.Reflection.PropertyInfo prop = typeof(CostInfoModel).GetProperty(Filter.OrderByPropertyName);
            if (prop == null)
            {
                return costInfos;
            }
            // todo ordering by Created is hardcoded here
            costInfos = Filter.OrderByDesc.Value ? costInfos.OrderByDescending(x => x.Created) : costInfos.OrderBy(x => x.Created);
            if (Filter.PageNumber != null)
            {
                costInfos = costInfos.Skip(Math.Max(0, Filter.PageNumber.Value - 1) * Filter.PageSize);
            }
            return costInfos.Take(Filter.PageSize);
        }
    }
}
