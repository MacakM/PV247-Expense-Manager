using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Accounts
{
    /// <summary>
    /// Filter userd in queries in order to get accounts with specifies parameters
    /// </summary>
    internal class AccountsByName : FilterValueBase<AccountModel, string>
    {
        public override Expression<Func<AccountModel, bool>> GetWhereCondition(string value)
            => account => account.Name.Contains(value);
    }
}
