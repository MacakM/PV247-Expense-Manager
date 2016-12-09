using System;
using System.Linq.Expressions;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Users
{
    /// <summary>
    /// Filter query by access type
    /// </summary>
    internal class UsersByAccessType : FilterValueBase<UserModel, AccountAccessTypeModel>
    {
        public override Expression<Func<UserModel, bool>> GetWhereCondition(AccountAccessTypeModel value)
            => user => user.AccessType == value;
    }
}