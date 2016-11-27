using System;
using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.CostInfos
{
    public class CostInfoModelsByCreatedFrom : IFilterModel<CostInfoModel>
    {
        /// <summary>
        /// Left edge of created range
        /// </summary>
        public DateTime? CreatedFrom { get; set; }


        public CostInfoModelsByCreatedFrom(DateTime? start)
        {
            this.start = start;
        }

        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            throw new NotImplementedException();
        }
    }
}
