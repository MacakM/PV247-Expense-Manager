namespace ExpenseManager.Database.DataAccess.FilterInterfaces
{
    /// <summary>
    /// Interface for classes that handles ordering and paging of IQueryable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPageAndOrderable<T> : IFilter<T>
    {
        /// <summary>
        /// Determines size of page, if there is no page number, all items will be taken
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// Determines how many pages to skip 
        /// </summary>
        int? PageNumber { get; set; }

        /// <summary>
        /// Determines if resulsts are ordered descending or ascending
        /// </summary>
        bool? OrderByDesc { get; set; }

        /// <summary>
        /// Determines property to order by
        /// </summary>
        string OrderByPropertyName { get; set; }
    }
}
