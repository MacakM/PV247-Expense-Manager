using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Users
{
    /// <summary>
    /// Filter used to filter users by their email
    /// </summary>
    internal class UsersByEmail : FilterValueBase<UserModel, string>
    {
        public override Expression<Func<UserModel, bool>> GetWhereCondition(string value)
         => user => user.Email.Contains(value);
    }
}