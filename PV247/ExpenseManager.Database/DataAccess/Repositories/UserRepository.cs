using System;
using System.Data.Entity;
using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for User entity.
    /// This type needs to stay public as it extends the base public class
    /// </summary>
    public class UserRepository : ExpenseManagerRepository<UserModel, Guid>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">unitOfWork provider</param>
        public UserRepository(IUnitOfWorkProvider provider) : base(provider) { }

        /// <summary>
        /// Gets currently signed user according to its email
        /// </summary>
        /// <param name="email">User unique email</param>
        /// <param name="includes">Property to include with obtained user</param>
        /// <returns>User with user details</returns>
        public UserModel GetUserByEmail(string email, params string[] includes)
        {
            IQueryable<UserModel> users = Context.Set<UserModel>();

            // Include all required properties
            users = includes.Aggregate(users, (current, include) => current.Include(include));

            return users.FirstOrDefault(usr => usr.Email.Equals(email));
        }
    }
}
