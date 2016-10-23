using BL.DTOs;
using BL.Infrastructure.Mapping;
using BL.Infrastructure.Services;
using BL.Repositories;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Services
{
    /// <summary>
    /// Provides user related functionality
    /// </summary>
    public class UserService : ExpenseManagerCrudServiceBase<User, int, UserDTO>
    {
        public UserService(IRepository<User, int> repository, IEntityDTOMapper<User, UserDTO> mapper) : base(repository, mapper) { }

        /// <summary>
        /// Registers user according to provided information
        /// </summary>
        /// <param name="userRegistration">User registration information</param>
        public void RegisterNewUser(UserDTO userRegistration)
        {
            Save(userRegistration);
        }

        /// <summary>
        /// Gets currently signed user according to its email
        /// </summary>
        /// <param name="email">User unique email</param>
        /// <returns>UserDTO with user details</returns>
        public UserDTO GetCurrentlySignedUser(string email)
        {
            User user;
            using (UnitOfWorkProvider.Create())
            {
                user = ((UserRepository) Repository).GetUserByEmail(email);
            }
            return Mapper.MapToDTO(user);
        }
    }
}
