using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.Users
{
    /// <summary>
    /// Filters by user name
    /// </summary>
    public class UserModelsByName : IFilter<UserModel>
    {
        /// <summary>
        /// User name
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Determines if Equals() or Contains() should be use while filtering with strings
        /// </summary>
        private readonly bool _doExactMatch;

        /// <summary>
        /// Filters by user name
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="doExactMatch">If apply exact match</param>
        public UserModelsByName(string name, bool doExactMatch = false)
        {
            _name = name;
            _doExactMatch = doExactMatch;
        }

        /// <summary>
        /// Filters by user name
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
            return _doExactMatch ? queryable.Where(user => user.Name.Equals(_name)) : queryable.Where(user => user.Name.Contains(_name));
        }
    }
}
