using System.Linq;

namespace ExpenseManager.Database.Filters
{
    /// <summary>
    /// General filter interface
    /// </summary>
    public interface IFilter<T>
    {
        /// <summary>
        /// In this method filter should apply himself on queryable
        /// </summary>
        /// <param name="queryable">Queryable</param>
        /// <returns></returns>
        IQueryable<T> FilterQuery(IQueryable<T> queryable);
    }
}
