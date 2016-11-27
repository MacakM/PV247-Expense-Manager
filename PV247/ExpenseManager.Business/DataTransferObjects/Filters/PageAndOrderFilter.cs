namespace ExpenseManager.Business.DataTransferObjects.Filters
{
    /// <summary>
    /// Filter that handles pages and ordering
    /// </summary>
    public class PageAndOrderFilter
    {
        private int _pageSize = 10;

        /// <summary>
        /// Determines size of page, if there is no page number, all items will be taken
        /// </summary>
        public int PageSize
        {
            get { return PageNumber == null ? int.MaxValue : _pageSize; }
            set { _pageSize = value; }
        }
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
