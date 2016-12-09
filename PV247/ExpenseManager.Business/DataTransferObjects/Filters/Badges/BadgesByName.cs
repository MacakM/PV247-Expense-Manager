using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Badges
{
    /// <summary>
    /// Filters by badge name
    /// </summary>
    internal class BadgesByName : FilterValueBase<BadgeModel, string>
    {
        public override Expression<Func<BadgeModel, bool>> GetWhereCondition(string value)
            => badge => badge.Name.Contains(value);

    }
}
