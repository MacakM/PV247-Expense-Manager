using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters cost by its income type
    /// </summary>
    internal class CostInfosByIsIncome : FilterValueBase<CostInfoModel, bool>
    {
        public override Expression<Func<CostInfoModel, bool>> GetWhereCondition(bool value)
            => costInfo => costInfo.IsIncome == value;
    }
}