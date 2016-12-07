using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Users
{
    /// <summary>
    /// Filters by user name
    /// </summary>
    internal class UsersByAccountName : IFilter<UserModel>
    {
        /// <summary>
        /// Specifies account name to filter with
        /// </summary>
        public readonly string AccountName;

        /// <summary>
        /// Filters by user name
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
            return AccountName != null ? queryable.Where(user => user.Account.Name.Contains(AccountName)) : queryable;
        }

        public UsersByAccountName(string accountName)
        {
            AccountName = accountName;
        }
    }
}
