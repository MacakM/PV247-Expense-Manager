using System;
using System.Linq;
using System.Linq.Expressions;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Helps to order queryable by property name
    /// Unfortunately this entire class must stay public since the new filter implementation uses it
    /// </summary>
    public class QueryOrderByHelper
    {
        /// <summary>
        /// Default order by
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(IQueryable<T> source, string propertyName)
        {
            return (IQueryable<T>)OrderBy((IQueryable)source, propertyName);
        }

        /// <summary>
        /// Default order by
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IQueryable OrderBy(IQueryable source, string propertyName)
        {
            var x = Expression.Parameter(source.ElementType);
            var selector = Expression.Lambda(Expression.PropertyOrField(x, propertyName), x);
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "OrderBy", new Type[] { source.ElementType, selector.Body.Type },source.Expression, selector));
        }

        /// <summary>
        /// Orders descending
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderByDesc<T>(IQueryable<T> source, string propertyName)
        {
            return (IQueryable<T>)OrderByDesc((IQueryable)source, propertyName);
        }

        /// <summary>
        /// Orders descending
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        internal static IQueryable OrderByDesc(IQueryable source, string propertyName)
        {
            var x = Expression.Parameter(source.ElementType);
            var selector = Expression.Lambda(Expression.PropertyOrField(x, propertyName), x);
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { source.ElementType, selector.Body.Type },source.Expression, selector));
        }
    }
}

