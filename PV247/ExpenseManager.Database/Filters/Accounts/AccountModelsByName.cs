using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.Accounts
{
    /// <summary>
    /// Filter userd in queries in order to get accounts with specifies parameters
    /// </summary>
    public class AccountModelsByName : IFilterModel<AccountModel>
    {
        /// <summary>
        /// Name that has to match in filtered accounts
        /// </summary>
        public string Name;

        /// <summary>
        /// Determines if Equals() or Contains() should be used when matching string parameters
        /// </summary>
        public bool DoExactMatch;

        /// <summary>
        /// Filters by account name
        /// </summary>
        /// <param name="name">Account name</param>
        /// <param name="doExactMatch">If apply exact match</param>
        public AccountModelsByName(string name, bool doExactMatch = false)
        {
            Name = name;
            DoExactMatch = doExactMatch;
        }

        /// <summary>
        /// Filters given query
        /// </summary>
        /// <param name="queryable">Query to be filtered</param>
        public IQueryable<AccountModel> FilterQuery(IQueryable<AccountModel> queryable)
        {
               return DoExactMatch ? queryable.Where(account => account.Name.Equals(Name)) : queryable.Where(account => account.Name.Contains(Name));
        }
    }
}
