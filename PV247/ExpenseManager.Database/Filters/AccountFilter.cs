namespace ExpenseManager.Database.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountFilter : FilterBase
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
