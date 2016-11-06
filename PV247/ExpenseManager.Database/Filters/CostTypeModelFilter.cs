namespace ExpenseManager.Database.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class CostTypeModelFilter : FilterModelBase
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
