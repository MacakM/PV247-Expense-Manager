using System.Linq;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters.Users
{
    /// <summary>
    /// Filter used to filter users by their email
    /// </summary>
    public class UserModelsByEmail : IFilter<UserModel>
    {
        /// <summary>
        /// Determines if Equals() or Contains() should be use while filtering with strings
        /// </summary>
        private readonly bool _doExactMatch;

        /// <summary>
        /// Specifies users email to filter with
        /// </summary>
        private readonly string _email;

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="doExactMatch"></param>
        /// <param name="email"></param>
        public UserModelsByEmail(string email, bool doExactMatch = false)
        {
            _doExactMatch = doExactMatch;
            _email = email;
        }

        /// <summary>
        /// Filters users by their email
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
            return _doExactMatch ? queryable.Where(user => user.Email.Equals(_email)) : queryable.Where(user => user.Email.Contains(_email));
        }
    }
}
