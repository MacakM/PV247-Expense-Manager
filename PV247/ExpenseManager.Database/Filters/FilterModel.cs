using System.Linq;

namespace ExpenseManager.Database.Filters
{
    /// <summary>
    /// General filter base class
    /// </summary>
    public abstract class FilterModel<T>
    {
        /// <summary>
        /// In this method filter should apply himself on queryable
        /// </summary>
        /// <param name="queryable">Queryable</param>
        /// <returns></returns>
        public abstract IQueryable<T> FilterQuery(IQueryable<T> queryable);
    }
}
