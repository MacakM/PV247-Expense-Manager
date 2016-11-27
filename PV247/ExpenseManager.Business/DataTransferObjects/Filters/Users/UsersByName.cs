namespace ExpenseManager.Business.DataTransferObjects.Filters.Users
{
    /// <summary>
    /// Filters by user name
    /// </summary>
    public class UserModelsByName : Filter<User>
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Determines if Equals() or Contains() should be use while filtering with strings
        /// </summary>
        public bool DoExactMatch { get; set; }

        /// <summary>
        /// Filters by user name
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="doExactMatch">If apply exact match</param>
        public UserModelsByName(string name, bool doExactMatch = false)
        {
            Name = name;
            DoExactMatch = doExactMatch;
        }
    }
}