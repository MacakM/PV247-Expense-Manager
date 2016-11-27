using System;
using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.CostInfos
{
    public class CostInfoModelsByPlannedTypeId : IFilterModel<CostInfoModel>
    {
        public Guid PlannedTypeId { get; set; }

        public CostInfoModelsByPlannedTypeId(Guid plannedTypeId)
        {
            this.plannedTypeId = plannedTypeId;
        }

        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            throw new NotImplementedException();
        }
    }
}
