using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Business.Utilities.BadgeCertification;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.DataAccess.Queries;
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

        private readonly ExpenseManagerRepository<AccountBadgeModel, Guid> _accountBadgeRepository;

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
        /// <param name="certifierResolver">Resolves badge certifiers according to badge name</param>
        /// <param name="accountBadgeRepository">Repository for accountBadges</param>
        /// <param name="badgesQuery">Query object for retrieving badges</param>
        /// <param name="accountsQuery">Query object for retrieving accounts</param>
        public BadgeService(ExpenseManagerQuery<BadgeModel> query, ExpenseManagerRepository<BadgeModel, Guid> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider, ListBadgesQuery badgesQuery, ListAccountsQuery accountsQuery, IBadgeCertifierResolver certifierResolver, ExpenseManagerRepository<AccountBadgeModel, Guid> accountBadgeRepository) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
            _badgesQuery = badgesQuery;
            _accountsQuery = accountsQuery;
            _certifierResolver = certifierResolver;
            _accountBadgeRepository = accountBadgeRepository;
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
        /// <param name="filters">Filters badges</param>
        /// <param name="pageAndOrder"></param>
        /// <returns></returns>
        public List<Badge> ListBadges(List<IFilter<BadgeModel>> filters, IPageAndOrderable<BadgeModel> pageAndOrder)
        {
            Query.Filters = filters;
            Query.PageAndOrderModelFilterModel = pageAndOrder;
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
                    foreach (var badge in allBadges)
                    {
                        var assignBadge = _certifierResolver.ResolveBadgeCertifier(badge.Name)
                            ?.CanAssignBadge(account) ?? false;

                        if (assignBadge)
                        {
                            _accountBadgeRepository.Insert(new AccountBadgeModel
                            {
                                Account = account,
                                Badge = badge,
                                Achieved = DateTime.Now
                            });
                        }
                    }
                }
                uow.Commit();
            }
        }
    }
}
