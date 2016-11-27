using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Business.Utilities.BadgeCertification;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// Service handles Badge entity operations
    /// </summary>
    public class BadgeService : ExpenseManagerQueryAndCrudServiceBase<BadgeModel, Guid, Badge>, IBadgeService
    {
        private readonly IBadgeCertifierResolver _certifierResolver;

        private readonly ListBadgesQuery _badgesQuery;

        private readonly ListAccountsQuery _accountsQuery;

        private readonly ExpenseManagerRepository<AccountModel, Guid> _accountRepository;

        /// <summary>
        /// Included entities
        /// </summary>
        protected override string[] EntityIncludes { get; } =
        {
            nameof(BadgeModel.Accounts)
        };

        /// <summary>
        /// Badge service constructor
        /// </summary>
        /// <param name="query">Query</param>
        /// <param name="repository">Repository</param>
        /// <param name="expenseManagerMapper">Mapper</param>
        /// <param name="unitOfWorkProvider">Unit of work provider</param>
        public BadgeService(ExpenseManagerQuery<BadgeModel> query, ExpenseManagerRepository<BadgeModel, Guid> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider, ListBadgesQuery badgesQuery, ListAccountsQuery accountsQuery, ExpenseManagerRepository<AccountModel, Guid> accountRepository, IBadgeCertifierResolver certifierResolver) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
            _badgesQuery = badgesQuery;
            _accountsQuery = accountsQuery;
            _accountRepository = accountRepository;
            _certifierResolver = certifierResolver;
        }

        /// <summary>
        /// Creates new Badge object in database
        /// </summary>
        public Guid CreateBadge(Badge badge)
        {
            return Save(badge);
        }

        /// <summary>
        /// Updates existing badge in database
        /// </summary>
        /// <param name="badgeEdited"></param>
        public void UpdateBadge(Badge badgeEdited)
        {
            Save(badgeEdited);
        }

        /// <summary>
        /// Deletes badge cpecified by id
        /// </summary>
        /// <param name="badgeId"></param>
        public void DeleteBadge(Guid badgeId)
        {
            Delete(badgeId);
        }

        /// <summary>
        /// Get specific badge by unique id
        /// </summary>
        /// <param name="badgeId"></param>
        /// <returns></returns>
        public Badge GetBadge(Guid badgeId)
        {
            return GetDetail(badgeId);
        }

        /// <summary>
        /// Lists filtered badges
        /// </summary>
        /// <param name="filter">Filters badges</param>
        /// <returns></returns>
        public List<Badge> ListBadges(BadgeFilter filter)
        {
            Query.Filter = ExpenseManagerMapper.Map<BadgeModelFilter>(filter);
            return GetList().ToList();
        }

        /// <summary>
        /// Check all accounts if they deserve to assign badges
        /// </summary>
        public void CheckBadgesRequirements()
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var allAccounts = _accountsQuery.Execute();
                var allBadges = _badgesQuery.Execute();
                foreach (var account in allAccounts)
                {
                    var initialBadgesCount = account.Badges.Count;
                    foreach (var badge in allBadges)
                    {
                        _certifierResolver.ResolveBadgeCertifier(badge.Name)
                            ?.CanAssignBadge(account);
                    }

                    // perform update only if badges were added
                    if (initialBadgesCount < account.Badges.Count)
                    {
                        _accountRepository.Update(account);
                    }                  
                }
                uow.Commit();
            }
        }
    }
}
