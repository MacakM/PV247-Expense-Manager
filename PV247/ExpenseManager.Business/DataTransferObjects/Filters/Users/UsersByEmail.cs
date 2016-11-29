using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Users
{
    /// <summary>
    /// Filter used to filter users by their email
    /// </summary>
    public class UsersByEmail : IFilter<UserModel>
    {
        /// <summary>
        /// Determines if Equals() or Contains() should be use while filtering with strings
        /// </summary>
        public bool DoExactMatch { get; set; }

        /// <summary>
        /// Specifies users email to filter with
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="doExactMatch"></param>
        /// <param name="email"></param>
        public UsersByEmail(string email, bool doExactMatch = false)
        {
            DoExactMatch = doExactMatch;
            Email = email;
        }

        /// <summary>
        /// Filters users by their email
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
            return DoExactMatch ? queryable.Where(user => user.Email.Equals(Email)) : queryable.Where(user => user.Email.Contains(Email));
        }
    }
}