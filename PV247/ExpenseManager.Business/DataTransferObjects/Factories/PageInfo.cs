namespace ExpenseManager.Business.DataTransferObjects.Factories
{
    /// <summary>
    /// PageInfo
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// Determines size of page, if there is no page number, all items will be taken
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Determines how many pages to skip 
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        /// Determines if resulsts are ordered descending or ascending
        /// </summary>
        public bool? OrderByDesc { get; set; }

        /// <summary>
        /// Determines property to order by
        /// </summary>
        public string OrderByPropertyName { get; set; }
    }
}
