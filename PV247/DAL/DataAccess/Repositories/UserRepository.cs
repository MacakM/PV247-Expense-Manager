using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.DataAccess.Repositories
{
    public class UserRepository : ExpenseManagerRepository<User, int>
    {
        public UserRepository(IUnitOfWorkProvider provider) : base(provider) { }

        private static readonly Expression<Func<User, object>>[] _includes = 
            {
                usr => usr.Badges,
                usr => usr.Costs,
                usr => usr.OwnPastes,
                usr => usr.Plans,
                usr => usr.VisiblePastes
            };

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

            var user = users.FirstOrDefault(usr => usr.Email.Equals(email));

            if (user == null)
            {
                Debug.WriteLine($"User with email {email} does not exists in the DB!");
            }
            return user;
        }

        /// <summary>
        /// Gets currently signed user (with all initialized properties) according to its email
        /// </summary>
        /// <param name="email">User unique email</param>
        /// <returns>UserDTO with user details</returns>
        public User GetUserByEmailIncludingAll(string email)
        {
            return GetUserByEmail(email, _includes);
        }
    }
}
