using System;
using System.Linq.Expressions;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters cost infos by periodicity
    /// </summary>
    internal class CostInfosByPeriodicity : FilterValueBase<CostInfoModel, PeriodicityModel>
    {
        public override Expression<Func<CostInfoModel, bool>> GetWhereCondition(PeriodicityModel value)
            => costInfo => costInfo.Periodicity == value;
    }
}
