using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// Service handles Account entity operations
    /// </summary>
    public class AccountService : ExpenseManagerQueryAndCrudServiceBase<AccountModel, int, Account>, IAccountService
    {
        private readonly UserRepository _userRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="repository"></param>
        /// <param name="expenseManagerMapper"></param>
        /// <param name="unitOfWorkProvider"></param>
        /// <param name="userRepository"></param>
        public AccountService(
            ExpenseManagerQuery<AccountModel> query, 
            ExpenseManagerRepository<AccountModel, int> repository, 
            Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider,
            UserRepository userRepository) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        protected override string[] EntityIncludes { get; } = new string[0];
        /// <summary>
        /// Creates new account
        /// </summary>
        /// <param name="account"></param>
        public void CreateAccount(Account account)
        {
            Save(account);
        }

        /// <summary>
        /// Creates account for user with given id
        /// </summary>
        public void CreateAccount(int userId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var user = _userRepository.GetById(userId);

                if (user == null)
                {
                    throw new InvalidOperationException("User with given ID doesn't exist");
                }

                var account = new AccountModel()
                {
                    Users = new List<UserModel>() { user },
                    Name = user.Name + "'s account"
                };

                Repository.Insert(account);
                uow.Commit();
            }
        }

        /// <summary>
        /// Updates existing account
        /// </summary>
        /// <param name="updatedAccount"></param>
        public void UpdateAccount(Account updatedAccount)
        {
            Save(updatedAccount);
        }
        /// <summary>
        /// Deletes account by specified unique id
        /// </summary>
        /// <param name="accountId"></param>
        public void DeleteAccount(int accountId)
        {
            Delete(accountId);
        }
        /// <summary>
        /// Get account specified by id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public Account GetAccount(int accountId)
        {
           return GetDetail(accountId);
        }
        /// <summary>
        /// List filtered accounts
        /// </summary>
        /// <param name="filter">Filters accounts</param>
        /// <returns></returns>
        public List<Account> ListAccounts(AccountFilter filter)
        {
            Query.Filter = ExpenseManagerMapper.Map<AccountModelFilter>(filter);
            return GetList().ToList();
        }

        /// <summary>
        /// Attaches account with given ID to user with given access type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accountId"></param>
        /// <param name="accessType"></param>
        public void AttachAccountToUser(int userId, int accountId, AccountAccessType accessType)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var user = _userRepository.GetById(userId);
                var account = Repository.GetById(accountId);
                user.Account = account;
                user.AccessType = (AccountAccessTypeModel)accessType;
                uow.Commit();
            }
        }
    }
}
