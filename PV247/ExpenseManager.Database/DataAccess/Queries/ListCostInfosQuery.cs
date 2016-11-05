using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    public class ListCostInfosQuery : ExpenseManagerQuery<CostInfoModel>
    {
        public ListCostInfosQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        public CostInfoFilter Filter { get; set; }

        protected override IQueryable<CostInfoModel> GetQueryable()
        {
            IQueryable<CostInfoModel> costInfos = Context.CostInfos;

            if (Filter == null)
            {
                return costInfos;
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
            return costInfos;
        }
    }
}
