using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.CostInfos
{
    /// <summary>
    /// Filtery by money to
    /// </summary>
    public class CostInfoModelsByMoneyTo : FilterModel<CostInfoModel>
    {
        /// <summary>
        /// Money to
        /// </summary>
        public decimal MoneyTo { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="moneyTo"></param>
        public CostInfoModelsByMoneyTo(decimal moneyTo)
        {
            MoneyTo = moneyTo;
        }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public override IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return queryable.Where(x => x.Money <= MoneyTo);
        }
    }
}