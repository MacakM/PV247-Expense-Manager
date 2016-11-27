using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Utils;
using Riganti.Utils.Infrastructure.Core;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// Provides user related functionality
    /// </summary>
    public class UserService : ExpenseManagerQueryAndCrudServiceBase<UserModel, Guid, User>, IUserService
    {
        private readonly UserRepository _userRepository;

        /// <summary>
        /// Please note that usage of concrete type for UserRepository is used on purpose
        /// </summary>
        /// <param name="query"></param>
        /// <param name="repository"></param>
        /// <param name="expenseManagerMapper"></param>
        /// <param name="unitOfWorkProvider"></param>
        public UserService(ExpenseManagerQuery<UserModel> query, UserRepository repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
            _userRepository = repository;
        }

        /// <summary>
        /// Entity includes
        /// </summary>
        protected override string[] EntityIncludes { get; } =
        {
            nameof(UserModel.Account)
        };

        /// <summary>
        /// Registers user according to provided information
        /// </summary>
        /// <param name="userRegistration">User registration information</param>
        public Guid RegisterNewUser(User userRegistration)
        {
            // create account too or join to another?
            return Save(userRegistration);
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
                var user = _userRepository.GetUserByEmail(modifiedUser.Email, EntityIncludes);
                if (user == null)
                {
                    throw new InvalidOperationException($"Cannot update user with email: { modifiedUser.Email }, the user is not persisted yet!");
                }
                _userRepository.Update(user);
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
                var entity = _userRepository.GetUserByEmail(email, IncludesHelper.ProcessIncludesList<User, UserModel>(includes));
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
                    _userRepository.GetUserByEmail(email, nameof(UserModel.Account)) :
                    _userRepository.GetUserByEmail(email);
                return ExpenseManagerMapper.Map<UserModel, User>(entity);
            }
        }

        /// <summary>
        /// List users that match parameters given in filter 
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="pageAndOrder"></param>
        /// <returns></returns>
        public List<User> ListUsers(List<IFilter<User>> filters, PageAndOrderFilter pageAndOrder)
        {
            Query.Filters = ExpenseManagerMapper.Map<List<IFilterModel<UserModel>>>(filters);
            Query.PageAndOrderModelFilterModel =
                ExpenseManagerMapper.Map<PageAndOrderModelFilterModel<UserModel>>(pageAndOrder);
            return GetList().ToList();
        }

        /// <summary>
        /// Get specific user that had id == userId
        /// </summary>
        /// <param name="userId">Unique user identifier</param>
        /// <returns>One user with id == userId</returns>
        public User GetUser(Guid userId)
        {
            return GetDetail(userId);
        }

        /// <summary>
        /// Delete user specified by userId
        /// </summary>
        /// <param name="userId">Unique user identifier</param>
        public void DeleteUser(Guid userId)
        {
            Delete(userId);
        }
    }
}
