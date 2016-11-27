using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    /// <summary>
    /// Service handles user entity operations
    /// </summary>
    public interface IUserService : IService
    {
        /// <summary>
        /// Registers user according to provided information
        /// </summary>
        /// <param name="userRegistration">User registration information</param>
        Guid RegisterNewUser(User userRegistration);

        /// <summary>
        /// Updates existing user according to provided information
        /// </summary>
        /// <param name="modifiedUser">Updated user information</param>
        void UpdateUser(User modifiedUser);

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

        /// <summary>
        /// List users that match parameters given in filter 
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="pageAndOrder"></param>
        /// <returns></returns>
        List<User> ListUsers(List<Filter<User>> filters, PageAndOrderFilter pageAndOrder);

        /// <summary>
        /// Get specific user that had id == userId
        /// </summary>
        /// <param name="userId">Unique user identifier</param>
        /// <returns>One user with id == userId</returns>
        User GetUser(Guid userId);

        /// <summary>
        /// Delete user specified by userId
        /// </summary>
        /// <param name="userId">Unique user identifier</param>
        void DeleteUser(Guid userId);
    }
}