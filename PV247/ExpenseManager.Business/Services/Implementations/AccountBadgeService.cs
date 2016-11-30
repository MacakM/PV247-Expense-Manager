using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;
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
        /// <param name="query">Query</param>
        /// <param name="repository">Repository</param>
        /// <param name="expenseManagerMapper">Mapper</param>
        /// <param name="unitOfWorkProvider">Unit of work provider</param>
        public AccountBadgeService(ExpenseManagerQuery<AccountBadgeModel> query, ExpenseManagerRepository<AccountBadgeModel, Guid> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
        }

        /// <summary>
        /// Included entities
        /// </summary>
        protected override string[] EntityIncludes { get; } = 
        {
            nameof(AccountBadgeModel.Account), 
            nameof(AccountBadgeModel.Badge)
        };

        /// <summary>
        /// Add new badge to account by creating new AccountBadge object in database
        /// </summary>
        /// <param name="accountBadge">Account badge</param>
        public void CreateAccountBadge(AccountBadge accountBadge)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Save(accountBadge);
                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Updates existing account badge
        /// </summary>
        /// <param name="updatedAccountBadge"></param>
        public void UpdateAccountBadge(AccountBadge updatedAccountBadge)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Save(updatedAccountBadge);
                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Deletes specified account badge
        /// </summary>
        /// <param name="accountBadgeId"></param>
        public void DeleteAccountBadge(Guid accountBadgeId)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Delete(accountBadgeId);
                unitOfWork.Commit();
            }        
        }

        /// <summary>
        /// Get account badge specified by id
        /// </summary>
        /// <param name="accountBadgeId"></param>
        /// <returns></returns>
        public AccountBadge GetAccountBadge(Guid accountBadgeId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return GetDetail(accountBadgeId);
            }            
        }

        /// <summary>
        /// List filtered account badges
        /// </summary>
        /// <param name="filters">Filters account badges</param>
        /// <param name="pageAndOrder"></param>
        /// <returns></returns>
        public List<AccountBadge> ListAccountBadges(List<IFilter<AccountBadgeModel>> filters, IPageAndOrderable<AccountBadgeModel> pageAndOrder)
        {
            Query.Filters = filters;
            Query.PageAndOrderModelFilterModel = pageAndOrder;
            using (UnitOfWorkProvider.Create())
            {
                return GetList().ToList();
            }           
        }
    }
}
