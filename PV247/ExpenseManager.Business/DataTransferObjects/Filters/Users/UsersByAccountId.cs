using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Users
{
    /// <summary>
    /// Filters by account id
    /// </summary>
    internal class UsersByAccountId : IFilter<UserModel>
    {
        /// <summary>
        /// Specifies account id to filter with
        /// </summary>
        public readonly Guid? AccountId;

        /// <summary>
        ///  Filters by account id
        /// </summary>
        /// <param name="queryable">queryable</param>
        /// <returns></returns>
        public IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
            return AccountId != null ? queryable.Where(user => user.Account.Id == AccountId) : queryable;
        }

        public UsersByAccountId(Guid? accountId)
        {
            AccountId = accountId;
        }
    }
}
