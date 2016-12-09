using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostTypes
{
    /// <summary>
    /// Filters by name
    /// </summary>
    internal class CostTypesByName : FilterValueBase<CostTypeModel, string>
    {
        public override Expression<Func<CostTypeModel, bool>> GetWhereCondition(string value)
            => costType => costType.Name.Contains(value);
    }
}
