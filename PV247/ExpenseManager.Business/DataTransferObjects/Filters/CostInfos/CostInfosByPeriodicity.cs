using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters cost infos by periodicity
    /// </summary>
    public class CostInfosByPeriodicity : IFilter<CostInfoModel>
    {
        /// <summary>
        /// Periodicity of cost 
        /// </summary>
        public PeriodicityModel Periodicity { get; set; }

        /// <summary>
        /// Filter construcor
        /// </summary>
        /// <param name="periodicity"></param>
        public CostInfosByPeriodicity(PeriodicityModel periodicity)
        {
            Periodicity = periodicity;
        }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return queryable.Where(x => x.Periodicity == Periodicity);
        }
    }
}
