using System;
using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters
{
    /// <summary>
    /// Base class of filter, handles paging and ordering
    /// </summary>
    public abstract class FilterModelBase<T>
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
        /// Filters given query
        /// </summary>
        /// <param name="queryable">Query to be filtered</param>
        public abstract IQueryable<T> FilterQuery(IQueryable<T> queryable);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public IQueryable<T> FilterRange(IQueryable<T> queryable, object from, object to, string propertyName)
        {
            return queryable;
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="doExactMatch"></param>
        /// <param name="filterValue"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public IQueryable<T> FilterString(IQueryable<T> queryable, bool doExactMatch, string filterValue, string propertyName)
        {
        
            return queryable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="filterValue"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public IQueryable<T> FilterByExactValue(IQueryable<T> queryable, object filterValue, string propertyName)
        {
            return queryable;
        }

    }
}
