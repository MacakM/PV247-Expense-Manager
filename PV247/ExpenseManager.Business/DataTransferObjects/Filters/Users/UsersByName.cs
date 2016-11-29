using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Users
{
    /// <summary>
    /// Filters by user name
    /// </summary>
    public class UserModelsByName : IFilter<UserModel>
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
        /// <summary>
        /// Filters by user name
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
            return DoExactMatch ? queryable.Where(user => user.Name.Equals(Name)) : queryable.Where(user => user.Name.Contains(Name));
        }
    }
}