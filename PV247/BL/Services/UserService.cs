using System;
using System.Diagnostics;
using System.Linq.Expressions;
using APILayer.DTOs;
using AutoMapper;
using BL.Infrastructure;
using DAL.DataAccess.Repositories;
using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Services
{
    /// <summary>
    /// Provides user related functionality
    /// </summary>
    public class UserService : ExpenseManagerCrudServiceBase<User, int, UserDTO>, IUserService
    {
        public UserService(IRepository<User, UserDTO, int> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) 
            : base(repository, expenseManagerMapper, unitOfWorkProvider) { }

        private UserRepository UserRepository => (UserRepository)Repository;

        /// <summary>
        /// Registers user according to provided information
        /// </summary>
        /// <param name="userRegistration">User registration information</param>
        public void RegisterNewUser(UserDTO userRegistration)
        {
            Save(userRegistration);
        }

        /// <summary>
        /// Updates existing user according to provided information
        /// </summary>
        /// <param name="modifiedUserDTO">Updated user information</param>
        public void UpdatesUser(UserDTO modifiedUserDTO)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                uow.RegisterAfterCommitAction(() => Debug.WriteLine($"Successfully modified user with email: {modifiedUserDTO.Email}"));
                var user = UserRepository.GetUserByEmailIncludingAll(modifiedUserDTO.Email);
                if (user == null)
                {
                    throw new InvalidOperationException($"Cannot update user with email: { modifiedUserDTO.Email }, the user is not persisted yet!");      
                }
                //user.Badges = ...

                UserRepository.Update(user);
                uow.Commit();
            }
        }

        /// <summary>
        /// Gets currently signed user according to its email
        /// </summary>
        /// <param name="email">User unique email</param>
        /// <param name="includes">Property to include with obtained user</param>
        /// <returns>UserDTO with user details</returns>
        public UserDTO GetCurrentlySignedUser(string email, params Expression<Func<User, object>>[] includes)
        {
            using (UnitOfWorkProvider.Create())
            {
                return UserRepository.GetUserByEmail(email);
            }          
        }

        /// <summary>
        /// Gets currently signed user according to its email
        /// </summary>
        /// <param name="email">User unique email</param>
        /// <param name="includeAllProperties">Decides whether all properties should be included</param>
        /// <returns>UserDTO with user details</returns>
        public UserDTO GetCurrentlySignedUser(string email, bool includeAllProperties = false)
        {
            /*using (UnitOfWorkProvider.Create())
            {
                var user = UserRepository.GetUserByEmailIncludingAll(email);
                user.Name = "Jon Doe";
                Repository.InsertOrUpdate(user);
                user = UserRepository.GetUserByEmailIncludingAll(email);
            }*/

            using (UnitOfWorkProvider.Create())
            {              
                return includeAllProperties ? 
                    UserRepository.GetUserByEmailIncludingAll(email) : 
                    UserRepository.GetUserByEmail(email);
            }
        }
    }
}
