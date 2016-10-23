using BL.DTOs;
using BL.Infrastructure.Facades;
using BL.Services;

namespace BL.Facades
{
    /// <summary>
    /// Provides access to user related functionality
    /// </summary>
    public class UserFacade : IExpenseManagerFacade
    {
        private readonly UserService _userService;

        public UserFacade(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Registers user according to provided information
        /// </summary>
        /// <param name="userRegistration">User registration information</param>
        public void RegisterNewUser(UserDTO userRegistration)
        {
            _userService.RegisterNewUser(userRegistration);
        }

        /// <summary>
        /// Gets currently signed user according to its email
        /// </summary>
        /// <param name="email">User unique email</param>
        /// <returns>UserDTO with user details</returns>
        public UserDTO GetCurrentlySignedUser(string email)
        {
            return _userService.GetCurrentlySignedUser(email);
        }
    }
}
