using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filtery by money to
    /// </summary>
    internal class CostInfosByMoneyTo : IFilter<CostInfoModel>
    {
        /// <summary>
        /// Money to
        /// </summary>
        public readonly decimal? MoneyTo;

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return MoneyTo != null ? queryable.Where(x => x.Money <= MoneyTo) : queryable;
        }

        public CostInfosByMoneyTo(decimal? moneyTo)
        {
            MoneyTo = moneyTo;
        }
    }
}
