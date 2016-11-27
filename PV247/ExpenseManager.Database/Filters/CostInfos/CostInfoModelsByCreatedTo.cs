using System;
using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.CostInfos
{
    public class CostInfoModelsByCreatedTo : IFilterModel<CostInfoModel>
    {
        private DateTime? deadline;

        public CostInfoModelsByCreatedTo(DateTime? deadline)
        {
            this.deadline = deadline;
        }

        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            throw new NotImplementedException();
        }
    }
}
