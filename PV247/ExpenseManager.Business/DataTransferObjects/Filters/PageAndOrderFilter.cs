using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.DataAccess.Queries;

namespace ExpenseManager.Business.DataTransferObjects.Filters
{
    /// <summary>
    /// Filter that handles pages and ordering
    /// </summary>
    public class PageAndOrderFilter<T> : IPageAndOrderable<T>
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

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable">Queryable</param>
        /// <returns></returns>
        public IQueryable<T> FilterQuery(IQueryable<T> queryable)
        {
            if (OrderByDesc == null || string.IsNullOrEmpty(OrderByPropertyName))
            {
                return queryable;
            }
            queryable = OrderByDesc.Value
                ? QueryOrderByHelper.OrderByDesc(queryable, OrderByPropertyName)
                : QueryOrderByHelper.OrderBy(queryable, OrderByPropertyName);
            if (PageNumber != null)
            {
                queryable = queryable.Skip(Math.Max(0, PageNumber.Value - 1) * PageSize);
            }
            return queryable.Take(PageSize);
        }
    }
}
