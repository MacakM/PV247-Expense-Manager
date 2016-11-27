using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.Users
{
    /// <summary>
    /// Filters by user name
    /// </summary>
    public class UserModelsByAccountName : IFilterModel<UserModel>
    {
        /// <summary>
        /// Determines if Equals() or Contains() should be use while filtering with strings
        /// </summary>
        public bool DoExactMatch { get; set; }

        /// <summary>
        /// Specifies account name to filter with
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="accountName">Account name</param>
        /// <param name="doExactMatch">If apply exact match</param>
        public UserModelsByAccountName(string accountName, bool doExactMatch = false)
        {
            DoExactMatch = doExactMatch;
            AccountName = accountName;
        }

        /// <summary>
        /// Filters by user name
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
                return  DoExactMatch ? queryable.Where(user => user.Account.Name.Equals(AccountName)) : queryable.Where(user => user.Account.Name.Contains(AccountName));
        }
    }
}
