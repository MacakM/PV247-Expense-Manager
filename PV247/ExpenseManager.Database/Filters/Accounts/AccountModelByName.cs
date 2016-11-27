using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.Accounts
{
    /// <summary>
    /// Filter userd in queries in order to get accounts with specifies parameters
    /// </summary>
    public class AccountModelByName : IFilter<AccountModel>
    {
        /// <summary>
        /// Name that has to match in filtered accounts
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Determines if Equals() or Contains() should be used when matching string parameters
        /// </summary>
        private readonly bool _doExactMatch;

        /// <summary>
        /// Filters by account name
        /// </summary>
        /// <param name="name">Account name</param>
        /// <param name="doExactMatch">If apply exact match</param>
        public AccountModelByName(string name, bool doExactMatch = false)
        {
            _name = name;
            _doExactMatch = doExactMatch;
        }

        /// <summary>
        /// Filters given query
        /// </summary>
        /// <param name="queryable">Query to be filtered</param>
        public IQueryable<AccountModel> FilterQuery(IQueryable<AccountModel> queryable)
        {
               return _doExactMatch ? queryable.Where(account => account.Name.Equals(_name)) : queryable.Where(account => account.Name.Contains(_name));
        }
    }
}
