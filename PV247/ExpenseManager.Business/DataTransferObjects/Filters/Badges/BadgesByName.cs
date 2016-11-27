namespace ExpenseManager.Business.DataTransferObjects.Filters.Badges
{
    /// <summary>
    /// Filters by badge name
    /// </summary>
    public class BadgesByName : IFilter<Badge>
    {
        /// <summary>
        /// Name of Badge
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Determines if Equals() or Contains() should be used when matching string parameters
        /// </summary>
        public bool DoExactMatch { get; set; }

        /// <summary>
        /// Constructor of filter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="doExactMatch"></param>
        public BadgesByName(string name, bool doExactMatch)
        {
            Name = name;
            DoExactMatch = doExactMatch;
        }
    }
}
