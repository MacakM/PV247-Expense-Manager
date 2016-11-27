using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Accounts
{
    /// <summary>
    /// Filter userd in queries in order to get accounts with specifies parameters
    /// </summary>
    public class AccountsByName : IFilter<Account>
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
        public AccountsByName(string name, bool doExactMatch = false)
        {
            Name = name;
            DoExactMatch = doExactMatch;
        }
    }
}
