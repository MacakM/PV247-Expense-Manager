using System;
using System.Linq.Expressions;
using ExpenseManager.Business.DataTransferObjects;

namespace ExpenseManager.Business.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Registers user according to provided information
        /// </summary>
        /// <param name="userRegistration">User registration information</param>
        void RegisterNewUser(User userRegistration);

        /// <summary>
        /// Updates existing user according to provided information
        /// </summary>
        /// <param name="modifiedUser">Updated user information</param>
        void UpdatesUser(User modifiedUser);

        /// <summary>
        /// Gets currently signed user according to its email
        /// </summary>
        /// <param name="email">User unique email</param>
        /// <param name="includes">Property to include with obtained user</param>
        /// <returns>User with user details</returns>
        User GetCurrentlySignedUser(string email, params Expression<Func<User, object>>[] includes);

        /// <summary>
        /// Gets currently signed user according to its email
        /// </summary>
        /// <param name="email">User unique email</param>
        /// <param name="includeAllProperties">Decides whether all properties should be included</param>
        /// <returns>User with user details</returns>
        User GetCurrentlySignedUser(string email, bool includeAllProperties = false);
    }
}