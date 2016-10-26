using System;
using System.Linq.Expressions;
using APILayer.DTOs;
using DAL.Entities;

namespace BL.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Registers user according to provided information
        /// </summary>
        /// <param name="userRegistration">User registration information</param>
        void RegisterNewUser(UserDTO userRegistration);

        /// <summary>
        /// Updates existing user according to provided information
        /// </summary>
        /// <param name="modifiedUserDTO">Updated user information</param>
        void UpdatesUser(UserDTO modifiedUserDTO);

        /// <summary>
        /// Gets currently signed user according to its email
        /// </summary>
        /// <param name="email">User unique email</param>
        /// <param name="includes">Property to include with obtained user</param>
        /// <returns>UserDTO with user details</returns>
        UserDTO GetCurrentlySignedUser(string email, params Expression<Func<User, object>>[] includes);

        /// <summary>
        /// Gets currently signed user according to its email
        /// </summary>
        /// <param name="email">User unique email</param>
        /// <param name="includeAllProperties">Decides whether all properties should be included</param>
        /// <returns>UserDTO with user details</returns>
        UserDTO GetCurrentlySignedUser(string email, bool includeAllProperties = false);
    }
}