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
        /// Updates existing user according to provided information
        /// </summary>
        /// <param name="modifiedUserDTO">Updated user information</param>
        public void UpdatesUser(UserDTO modifiedUserDTO)
        {
            _userService.UpdatesUser(modifiedUserDTO);
        }

        /// <summary>
        /// Gets currently signed user according to its email
        /// </summary>
        /// <param name="email">User unique email</param>
        /// <param name="includeAllProperties">Decides whether all properties should be included</param>
        /// <returns>UserDTO with user details</returns>
        public UserDTO GetCurrentlySignedUser(string email, bool includeAllProperties = false)
        {
            return _userService.GetCurrentlySignedUser(email, includeAllProperties);
        }
    }
}
