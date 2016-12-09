using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters cost info by its creation time
    /// </summary>
    internal class CostInfosByCreatedFrom : FilterValueBase<CostInfoModel, DateTime>
    {
        public override Expression<Func<CostInfoModel, bool>> GetWhereCondition(DateTime value)
            => costInfo => costInfo.Created >= value;
    }
}
