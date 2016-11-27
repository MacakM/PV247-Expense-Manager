namespace ExpenseManager.Business.DataTransferObjects.Filters.CostTypes
{
    /// <summary>
    /// Filters by name
    /// </summary>
    public class CostTypesByName : Filter<CostType>
    {
        /// <summary>
        /// Used for filtering based on cost type name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Determines if Equals() or Contains() should be used when matching string parameters
        /// </summary>
        public bool DoExactMatch { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="doExactMatch"></param>
        public CostTypesByName(string name, bool doExactMatch)
        {
            Name = name;
            DoExactMatch = doExactMatch;
        }
    }
}
