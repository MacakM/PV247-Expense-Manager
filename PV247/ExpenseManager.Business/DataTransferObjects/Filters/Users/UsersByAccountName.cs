using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Users
{
    /// <summary>
    /// Filters by user name
    /// </summary>
    internal class UsersByAccountName : FilterValueBase<UserModel, string>
    {
        public override Expression<Func<UserModel, bool>> GetWhereCondition(string value)
            => user => user.Account.Name.Contains(value);
    }
}
