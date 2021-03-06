﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Utils;
using Riganti.Utils.Infrastructure.Core;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Infrastructure.Query;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// Provides user related functionality
    /// </summary>
    internal class UserService : ExpenseManagerQueryAndCrudServiceBase<UserModel, Guid, User>, IUserService
    {
        private readonly UserRepository _userRepository;

        /// <summary>
        /// Please note that usage of concrete type for UserRepository is used on purpose
        /// </summary>
        /// <param name="query"></param>
        /// <param name="repository"></param>
        /// <param name="expenseManagerMapper"></param>
        /// <param name="unitOfWorkProvider"></param>
        internal UserService(ExpenseManagerQuery<UserModel> query, UserRepository repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
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
        public void RegisterNewUser(User userRegistration)
        {
            var userEntity = ExpenseManagerMapper.Map<UserModel>(userRegistration);
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Insert(userEntity);
                unitOfWork.Commit();
            }
            userRegistration.Id = userEntity.Id;
        }

        /// <summary>
        /// Updates existing user according to provided information
        /// </summary>
        /// <param name="modifiedUser">Updated user information</param>
        public void UpdateUser(User modifiedUser)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                unitOfWork.RegisterAfterCommitAction(() => Debug.WriteLine($"Successfully modified user with email: {modifiedUser.Email}"));
                var user = _userRepository.GetUserByEmail(modifiedUser.Email, EntityIncludes);
                if (user == null)
                {
                    throw new InvalidOperationException($"Cannot update user with email: { modifiedUser.Email }, the user is not persisted yet!");
                }
                _userRepository.Update(user);
                unitOfWork.Commit();
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
        public List<User> ListUsers(IEnumerable<IFilter<UserModel>> filters, IPageAndOrderable<UserModel> pageAndOrder)
        {
            Query.Filters = filters;
            Query.PageAndOrderModelFilterModel = pageAndOrder;
            using (UnitOfWorkProvider.Create())
            {
                return GetList().ToList();
            }            
        }

        /// <summary>
        /// Get specific user that had id == userId
        /// </summary>
        /// <param name="userId">Unique user identifier</param>
        /// <returns>One user with id == userId</returns>
        public User GetUser(Guid userId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return GetDetail(userId);
            }           
        }

        /// <summary>
        /// Delete user specified by userId
        /// </summary>
        /// <param name="userId">Unique user identifier</param>
        public void DeleteUser(Guid userId)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Delete(userId);
                unitOfWork.Commit();
            }           
        }
    }
}
