using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostTypes
{
    internal class CostTypesByAccountId : FilterValueBase<CostTypeModel, Guid>
    {
        public override Expression<Func<CostTypeModel, bool>> GetWhereCondition(Guid value)
            => costType => costType.AccountId == value;
    }
}
