using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for User entity.
    /// </summary>
    public class UserRepository : ExpenseManagerRepository<User, int>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public UserRepository(IUnitOfWorkProvider provider) : base(provider) { }

        /// <summary>
        /// Gets currently signed user according to its email
        /// </summary>
        /// <param name="email">User unique email</param>
        /// <param name="includes">Property to include with obtained user</param>
        /// <returns>UserDTO with user details</returns>
        public User GetUserByEmail(string email, params Expression<Func<User, object>>[] includes)
        {
            IQueryable<User> users = Context.Set<User>();

            // Include all required properties
            users = includes.Aggregate(users, (current, include) => current.Include(include));

            return users.FirstOrDefault(usr => usr.Email.Equals(email));
        }
    }
}
