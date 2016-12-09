using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters by account id
    /// </summary>
    internal class CostInfosByAccountId : FilterValueBase<CostInfoModel, Guid>
    {
        public override Expression<Func<CostInfoModel, bool>> GetWhereCondition(Guid value)
            => costInfo => costInfo.AccountId == value;

    }
}