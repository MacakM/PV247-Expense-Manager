using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.CostInfos
{
    /// <summary>
    /// Filters cost by its income type
    /// </summary>
    public class CostInfoModelsByIsIncome : FilterModel<CostInfoModel>
    {
        /// <summary>
        /// If cost type is income or ourcome
        /// </summary>
        public bool IsIncome { get; set; }

        /// <summary>
        /// Filters constructor
        /// </summary>
        /// <param name="isIncome"></param>
        public CostInfoModelsByIsIncome(bool isIncome)
        {
            IsIncome = isIncome;
        }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public override IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return queryable.Where(x => x.IsIncome == IsIncome);
        }
    }
}
