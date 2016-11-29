using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filtery by money to
    /// </summary>
    public class CostInfosByMoneyTo : IFilter<CostInfoModel>
    {
        /// <summary>
        /// Money to
        /// </summary>
        public decimal MoneyTo { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="moneyTo"></param>
        public CostInfosByMoneyTo(decimal moneyTo)
        {
            MoneyTo = moneyTo;
        }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return queryable.Where(x => x.Money <= MoneyTo);
        }
    }
}
