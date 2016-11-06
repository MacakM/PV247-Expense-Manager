namespace ExpenseManager.Database.Filters
{
    /// <summary>
    /// Filter userd in queries in order to get accounts with specifies parameters
    /// </summary>
    public class AccountModelFilter : FilterModelBase
    {
        /// <summary>
        /// Name that has to match in filtered accounts
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Determines if Equals() or Contains() should be used when matching string parameters
        /// </summary>
        public bool DoExactMatch { get; set; }
    }
}
