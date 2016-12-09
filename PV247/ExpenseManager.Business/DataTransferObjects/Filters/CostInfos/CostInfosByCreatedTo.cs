using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters by time of cost info creation
    /// </summary>
    internal class CostInfosByCreatedTo : FilterValueBase<CostInfoModel, DateTime>
    {
        public override Expression<Func<CostInfoModel, bool>> GetWhereCondition(DateTime value)
            => costInfo => costInfo.Created <= value;
    }
}
