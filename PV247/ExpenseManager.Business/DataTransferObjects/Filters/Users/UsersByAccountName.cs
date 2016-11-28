namespace ExpenseManager.Business.DataTransferObjects.Filters.Users
{
    /// <summary>
    /// Filters by user name
    /// </summary>
    public class UsersByAccountName : Filter<User>
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
        /// Filters by user name 
        /// </summary>
        /// <param name="accountName">Account name</param>
        /// <param name="doExactMatch">Do exact match</param>
        public UsersByAccountName(string accountName, bool doExactMatch = false)
        {
            DoExactMatch = doExactMatch;
            AccountName = accountName;
        }
    }
}
