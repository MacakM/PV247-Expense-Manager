using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;
using ExpenseManager.Database.Infrastructure.Query;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// Service handles Account entity operations
    /// </summary>
    internal class AccountService : ExpenseManagerQueryAndCrudServiceBase<AccountModel, Guid, Account>, IAccountService
    {
        private readonly UserRepository _userRepository;

        /// <summary>
        /// Account service
        /// </summary>
        /// <param name="query">Query</param>
        /// <param name="repository">Account repository</param>
        /// <param name="expenseManagerMapper">Mapper</param>
        /// <param name="unitOfWorkProvider">Unit of work provider</param>
        /// <param name="userRepository">User repository</param>
        internal AccountService(
            ExpenseManagerQuery<AccountModel> query, 
            ExpenseManagerRepository<AccountModel, Guid> repository, 
            Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider,
            UserRepository userRepository) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Included entities
        /// </summary>
        protected override string[] EntityIncludes { get; } = 
        {
            nameof(AccountModel.Badges),
            nameof(AccountModel.Costs),
            nameof(AccountModel.Plans),
            nameof(AccountModel.Users)
        };

        /// <summary>
        /// Creates new account
        /// </summary>
        /// <param name="account"></param>
        public void CreateAccount(Account account)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Save(account);
                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Creates account for user with given id
        /// </summary>
        public Guid CreateAccount(Guid userId)
        {
            Guid accountId;
            using (var unitOfWork = UnitOfWorkProvider.Create())
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
                user.AccessType = AccountAccessTypeModel.Full;
                unitOfWork.Commit();
                accountId = account.Id;
            }
            return accountId;
        }

        /// <summary>
        /// Updates existing account
        /// </summary>
        /// <param name="updatedAccount"></param>
        public void UpdateAccount(Account updatedAccount)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Save(updatedAccount);
                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Deletes account by specified unique id
        /// </summary>
        /// <param name="accountId"></param>
        public void DeleteAccount(Guid accountId)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Delete(accountId);
                unitOfWork.Commit();
            }           
        }

        /// <summary>
        /// Get account specified by id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public Account GetAccount(Guid accountId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return GetDetail(accountId);
            }           
        }

        /// <summary>
        /// List filtered accounts
        /// </summary>
        /// <param name="filters">Filters accounts</param>
        /// <param name="pageAndOrder"></param>
        /// <returns></returns>
        public List<Account> ListAccounts(List<IFilter<AccountModel>> filters, IPageAndOrderable<AccountModel> pageAndOrder)
        {
            Query.Filters = filters;
            Query.PageAndOrderModelFilterModel = pageAndOrder;
            using (UnitOfWorkProvider.Create())
            {
                return GetList().ToList();
            }         
        }

        /// <summary>
        /// Attaches account with given ID to user with given access type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accountId"></param>
        /// <param name="accessType"></param>
        public void AttachAccountToUser(Guid userId, Guid accountId, AccountAccessType accessType)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                var user = _userRepository.GetById(userId);
                var account = Repository.GetById(accountId);
                user.Account = account;
                user.AccessType = (AccountAccessTypeModel)accessType;
                unitOfWork.Commit();
            }
        }
    }
}
