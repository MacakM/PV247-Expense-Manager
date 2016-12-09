using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Users
{
    /// <summary>
    /// Filters by account id
    /// </summary>
    internal class UsersByAccountId : FilterValueBase<UserModel, Guid>
    {
        public override Expression<Func<UserModel, bool>> GetWhereCondition(Guid value)
            => user => user.Account.Id == value;
    }
}
