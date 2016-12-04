using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filtery by money from
    /// </summary>
    internal class CostInfosByMoneyFrom : IFilter<CostInfoModel>
    {
        /// <summary>
        /// Money from
        /// </summary>
        public decimal MoneyFrom { get; set; }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return queryable.Where(x => x.Money >= MoneyFrom);
        }

    }
}