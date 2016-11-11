using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// Service handles Account entity operations
    /// </summary>
    public class AccountService : ExpenseManagerQueryAndCrudServiceBase<AccountModel, int, Account, AccountModelFilter>, IAccountService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="repository"></param>
        /// <param name="expenseManagerMapper"></param>
        /// <param name="unitOfWorkProvider"></param>
        public AccountService(ExpenseManagerQuery<AccountModel, AccountModelFilter> query, ExpenseManagerRepository<AccountModel, int> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
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


      
    }
}
