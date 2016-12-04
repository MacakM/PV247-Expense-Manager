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
        public Guid AccountId { get; set; }

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
