using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters cost by its income type
    /// </summary>
    internal class CostInfosByIsIncome : IFilter<CostInfoModel>
    {
        /// <summary>
        /// If cost type is income or ourcome
        /// </summary>
        public readonly bool? IsIncome;

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return IsIncome != null ? queryable.Where(x => x.IsIncome == IsIncome) : queryable;
        }

        public CostInfosByIsIncome(bool? isIncome)
        {
            IsIncome = isIncome;
        }

    }
}