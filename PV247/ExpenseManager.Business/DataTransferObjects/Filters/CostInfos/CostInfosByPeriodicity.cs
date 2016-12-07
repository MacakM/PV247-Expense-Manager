using System.Linq;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters cost infos by periodicity
    /// </summary>
    internal class CostInfosByPeriodicity : IFilter<CostInfoModel>
    {
        /// <summary>
        /// Periodicity of cost 
        /// </summary>
        public readonly PeriodicityModel? Periodicity;

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return Periodicity != null ? queryable.Where(x => x.Periodicity == Periodicity) : queryable;
        }

        public CostInfosByPeriodicity(PeriodicityModel? periodicity)
        {
            Periodicity = periodicity;
        }
        public CostInfosByPeriodicity(Periodicity? periodicity)
        {
            if (periodicity != null) Periodicity = (PeriodicityModel) periodicity;
        }
    }
}
