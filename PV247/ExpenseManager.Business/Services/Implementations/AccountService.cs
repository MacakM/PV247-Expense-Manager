using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountService : ExpenseManagerQueryAndCrudServiceBase<AccountModel, int, ListAccountsQuery, Account, AccountModelFilter>, IAccountService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="repository"></param>
        /// <param name="expenseManagerMapper"></param>
        /// <param name="unitOfWorkProvider"></param>
        public AccountService(ListAccountsQuery query, ExpenseManagerRepository<AccountModel, int> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        protected override string[] EntityIncludes { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        public void CreateAccount(Account account)
        {
            Save(account);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedAccount"></param>
        public void UpdateAccount(Account updatedAccount)
        {
            Save(updatedAccount);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        public void DeleteAccount(int accountId)
        {
            Delete(accountId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public Account GetAccount(int accountId)
        {
           return GetAccount(accountId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Account> ListAccounts(AccountFilter filter)
        {
            Query.Filter = ExpenseManagerMapper.Map<AccountModelFilter>(filter);
            return GetList().ToList();
        }


      
    }
}
