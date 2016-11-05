namespace ExpenseManager.Database.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountModelFilter : FilterModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool DoExactMatch { get; set; }
    }
}
