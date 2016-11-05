namespace ExpenseManager.Database.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class BadgeModelFilter : FilterModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool DoExactMatch { get; set; }
    }
}
