using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.Users
{
    /// <summary>
    /// Filters by user name
    /// </summary>
    public class UserModelsByAccountName : IFilter<UserModel>
    {
        /// <summary>
        /// Determines if Equals() or Contains() should be use while filtering with strings
        /// </summary>
        private readonly bool _doExactMatch;

        /// <summary>
        /// Specifies account name to filter with
        /// </summary>
        private readonly string _accountName;

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="accountName">Account name</param>
        /// <param name="doExactMatch">If apply exact match</param>
        public UserModelsByAccountName(string accountName, bool doExactMatch = false)
        {
            _doExactMatch = doExactMatch;
            _accountName = accountName;
        }

        /// <summary>
        /// Filters by user name
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
                return  _doExactMatch ? queryable.Where(user => user.Account.Name.Equals(_accountName)) : queryable.Where(user => user.Account.Name.Contains(_accountName));
        }
    }
}
