using System;
using System.Diagnostics;
using System.Linq.Expressions;
using AutoMapper;
using ExpenseManager.Business.DTOs;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using ExpenseManager.Database.Infrastructure.Utils;
using Riganti.Utils.Infrastructure.Core;
using ExpenseManager.Business.Services.Interfaces;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// Provides user related functionality
    /// </summary>
    public class UserService : ExpenseManagerCrudServiceBase<User, int, UserDTO>, IUserService
    {
        public UserService(ExpenseManagerRepository<User, int> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) 
            : base(repository, expenseManagerMapper, unitOfWorkProvider) { }

        private UserRepository UserRepository => (UserRepository)Repository;

        /// <summary>
        /// Registers user according to provided information
        /// </summary>
        /// <param name="userRegistration">User registration information</param>
        public void RegisterNewUser(UserDTO userRegistration)
        {
            // create account too or join to another?
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
                var user = UserRepository.GetUserByEmail(modifiedUserDTO.Email, EntityIncludes);
                if (user == null)
                {
                    throw new InvalidOperationException($"Cannot update user with email: { modifiedUserDTO.Email }, the user is not persisted yet!");
                }
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
        public UserDTO GetCurrentlySignedUser(string email, params Expression<Func<UserDTO, object>>[] includes)
        {
            using (UnitOfWorkProvider.Create())
            {
                var entity = UserRepository.GetUserByEmail(email, IncludesHelper.ProcessIncludesList<UserDTO, User>(includes));
                return ExpenseManagerMapper.Map<User, UserDTO>(entity);
            }          
        }

        /// <summary>
        /// Gets currently signed user according to its email
        /// </summary>
        /// <param name="email">User unique email</param>
        /// <param name="includeAllProperties">Decides whether all properties should be included</param>
        /// <returns>UserDTO with user details</returns>
        public UserDTO GetCurrentlySignedUser(string email, bool includeAllProperties = true)
        {
            using (UnitOfWorkProvider.Create())
            {              
                var entity = includeAllProperties ? 
                    UserRepository.GetUserByEmail(email, nameof(User.Account)) : 
                    UserRepository.GetUserByEmail(email);
                return ExpenseManagerMapper.Map<User, UserDTO>(entity);
            }
        }

        protected override string[] EntityIncludes { get; } =
        {
            nameof(User.Account)
        };
    }
}
