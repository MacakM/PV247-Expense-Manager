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
        public decimal MoneyTo { get; set; }

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
