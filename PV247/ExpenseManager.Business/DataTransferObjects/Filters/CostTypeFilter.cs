namespace ExpenseManager.Business.DataTransferObjects.Filters
{
    /// <summary>
    /// Filter userd in queries in order to get cost types with specifies parameters
    /// </summary>
    public class CostTypeFilter : FilterBase
    {
        /// <summary>
        /// Used for filtering based on cost type name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Determines if Equals() or Contains() should be used when matching string parameters
        /// </summary>
        public bool DoExactMatch { get; set; }
    }
}
