using System;
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
    /// Service handles AccountBadge entity operations
    /// </summary>
    public class AccountBadgeService : ExpenseManagerQueryAndCrudServiceBase<AccountBadgeModel, Guid, AccountBadge>, IAccountBadgeService
    {

        /// <summary>
        /// Service constructor takes service specific query base class props
        /// </summary>
        /// <param name="query"></param>
        /// <param name="repository"></param>
        /// <param name="expenseManagerMapper"></param>
        /// <param name="unitOfWorkProvider"></param>
        public AccountBadgeService(ExpenseManagerQuery<AccountBadgeModel> query, ExpenseManagerRepository<AccountBadgeModel, Guid> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        protected override string[] EntityIncludes { get; } = new string[0];
        /// <summary>
        /// Add new badge to account by creating new AccountBadge object in database
        /// </summary>
        /// <param name="accountBadge"></param>
        public Guid CreateAccountBadge(AccountBadge accountBadge)
        {
            return Save(accountBadge);
        }
        /// <summary>
        /// Updates existing account badge
        /// </summary>
        /// <param name="updatedAccountBadge"></param>
        public void UpdateAccountBadge(AccountBadge updatedAccountBadge)
        {
            Save(updatedAccountBadge);
        }
        /// <summary>
        /// Deletes specified account badge
        /// </summary>
        /// <param name="accountBadgeId"></param>
        public void DeleteAccountBadge(Guid accountBadgeId)
        {
            Delete(accountBadgeId);
        }
        /// <summary>
        /// Get account badge specified by id
        /// </summary>
        /// <param name="accountBadgeId"></param>
        /// <returns></returns>
        public AccountBadge GetAccountBadge(Guid accountBadgeId)
        {
            return GetDetail(accountBadgeId);
        }
        /// <summary>
        /// List filtered account badges
        /// </summary>
        /// <param name="filter">Filters account badges</param>
        /// <returns></returns>
        public List<AccountBadge> ListAccountBadges(AccountBadgeFilter filter)
        {
            Query.Filter = ExpenseManagerMapper.Map<AccountBadgeModelFilter>(filter);
            return GetList().ToList();
        }

    }
}
