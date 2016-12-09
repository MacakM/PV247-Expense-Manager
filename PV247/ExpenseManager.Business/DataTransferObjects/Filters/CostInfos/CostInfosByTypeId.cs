using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters by planned type id
    /// </summary>
    internal class CostInfosByTypeId : FilterValueBase<CostInfoModel, Guid>
    { 
        public override Expression<Func<CostInfoModel, bool>> GetWhereCondition(Guid value)
            => costInfo => costInfo.TypeId == value;
    }
}
