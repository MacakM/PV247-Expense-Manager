using System;
using System.Diagnostics;
using System.Linq.Expressions;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
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
    public class UserService : ExpenseManagerCrudServiceBase<UserModel, int, User>, IUserService
    {
        public UserService(ExpenseManagerRepository<UserModel, int> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) 
            : base(repository, expenseManagerMapper, unitOfWorkProvider) { }

        private UserRepository UserRepository => (UserRepository)Repository;

        /// <summary>
        /// Registers user according to provided information
        /// </summary>
        /// <param name="userRegistration">User registration information</param>
        public void RegisterNewUser(User userRegistration)
        {
            // create account too or join to another?
            Save(userRegistration);
        }

        /// <summary>
        /// Updates existing user according to provided information
        /// </summary>
        /// <param name="modifiedUser">Updated user information</param>
        public void UpdateUser(User modifiedUser)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                uow.RegisterAfterCommitAction(() => Debug.WriteLine($"Successfully modified user with email: {modifiedUser.Email}"));
                var user = UserRepository.GetUserByEmail(modifiedUser.Email, EntityIncludes);
                if (user == null)
                {
                    throw new InvalidOperationException($"Cannot update user with email: { modifiedUser.Email }, the user is not persisted yet!");
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
        /// <returns>User with user details</returns>
        public User GetCurrentlySignedUser(string email, params Expression<Func<User, object>>[] includes)
        {
            using (UnitOfWorkProvider.Create())
            {
                var entity = UserRepository.GetUserByEmail(email, IncludesHelper.ProcessIncludesList<User, UserModel>(includes));
                return ExpenseManagerMapper.Map<UserModel, User>(entity);
            }          
        }

        /// <summary>
        /// Gets currently signed user according to its email
        /// </summary>
        /// <param name="email">User unique email</param>
        /// <param name="includeAllProperties">Decides whether all properties should be included</param>
        /// <returns>User with user details</returns>
        public User GetCurrentlySignedUser(string email, bool includeAllProperties = true)
        {
            using (UnitOfWorkProvider.Create())
            {              
                var entity = includeAllProperties ? 
                    UserRepository.GetUserByEmail(email, nameof(UserModel.Account)) : 
                    UserRepository.GetUserByEmail(email);
                return ExpenseManagerMapper.Map<UserModel, User>(entity);
            }
        }

        protected override string[] EntityIncludes { get; } =
        {
            nameof(UserModel.Account)
        };
    }
}
