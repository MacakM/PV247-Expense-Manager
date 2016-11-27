using System;
using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.Users
{
    /// <summary>
    /// Filters by account id
    /// </summary>
    public class UserModelsByAccountId : IFilterModel<UserModel>
    {
        /// <summary>
        /// Specifies account id to filter with
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Filters by account id
        /// </summary>
        /// <param name="accountId"></param>
        public UserModelsByAccountId(Guid accountId)
        {
            AccountId = accountId;
        }

        /// <summary>
        ///  Filters by account id
        /// </summary>
        /// <param name="queryable">queryable</param>
        /// <returns></returns>
        public IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
             return queryable.Where(user => user.Account.Id == AccountId);
        }
    }
}
