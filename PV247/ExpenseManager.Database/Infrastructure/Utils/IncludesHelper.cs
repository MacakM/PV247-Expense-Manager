using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace ExpenseManager.Database.Infrastructure.Utils
{
    /// <summary>
    /// Helper class for converting DTO property includes into entity property paths
    /// </summary>
    public static class IncludesHelper
    {
        private const char ExpressionSeparator = '.';

        /// <summary>
        /// Process list of includes.
        /// </summary>
        /// <param name="includes">includes</param>
        public static string[] ProcessIncludesList<TDTO,TEntity>(Expression<Func<TDTO, object>>[] includes)
        {
            var includeList = new List<string>();
            foreach (var expressionBodyData in includes
                .Select(include => include.Body.ToString())
                .Where(expressionBodyString => expressionBodyString.Contains(ExpressionSeparator))
                .Select(expressionBodyString => expressionBodyString.Split(ExpressionSeparator)))
            {
                if (expressionBodyData.Length != 2)
                {
                    Debug.WriteLine("GetByIds(...) - includes do not currently support multiple nesting");
                    continue;
                }
                includeList.Add(expressionBodyData[1]);
            }
            return CheckIncludes<TEntity>(includeList).ToArray();
        }

        private static IEnumerable<string> CheckIncludes<TEntity>(List<string> includeList)
        {
            var entityPropertyNames = typeof(TEntity).GetProperties().Select(propInfo => propInfo.Name);
            var checkedIncludes = includeList.Where(include => entityPropertyNames.Contains(include)).ToList();
            var badIncludes = includeList.Except(checkedIncludes).ToList();
            foreach (var badInclude in badIncludes)
            {
                Debug.WriteLine($"WARNING: Property named {badInclude} does not exists.");
            }
            return includeList;
        }
    }
}
