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
        /// Specifies users email to filter with
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="email"></param>
        public UsersByEmail(string email)
        {
            Email = email;
        }

        /// <summary>
        /// Filters users by their email
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
            return queryable.Where(user => user.Email.Contains(Email));
        }
    }
}