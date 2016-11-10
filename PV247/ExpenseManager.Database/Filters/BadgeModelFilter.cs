namespace ExpenseManager.Database.Filters
{

    /// <summary>
    /// Filter userd in queries in order to get badges with specifies parameters
    /// </summary>
    public class BadgeModelFilter : FilterModelBase
    {
        /// <summary>
        /// Name of Badge
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description to be filtered with
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Determines if Equals() or Contains() should be used when matching string parameters
        /// </summary>
        public bool DoExactMatch { get; set; }
    }
}
