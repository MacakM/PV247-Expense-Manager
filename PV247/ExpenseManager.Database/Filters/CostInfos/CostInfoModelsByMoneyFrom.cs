using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.CostInfos
{
    /// <summary>
    /// Filtery by money from
    /// </summary>
    public class CostInfoModelsByMoneyFrom : FilterModel<CostInfoModel>
    {
        /// <summary>
        /// Money from
        /// </summary>
        public decimal MoneyFrom { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="moneyFrom"></param>
        public CostInfoModelsByMoneyFrom(decimal moneyFrom)
        {
            MoneyFrom = moneyFrom;
        }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public override IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return queryable.Where(x => x.Money >= MoneyFrom);
        }
    }
}
