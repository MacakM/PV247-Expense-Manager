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
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// Service handles Badge entity operations
    /// </summary>
    public class BadgeService : ExpenseManagerQueryAndCrudServiceBase<BadgeModel, Guid, Badge>, IBadgeService
    {
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
        public BadgeService(ExpenseManagerQuery<BadgeModel> query, ExpenseManagerRepository<BadgeModel, Guid> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
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
        public List<Badge> ListBadges(List<IFilter<Badge>> filters, PageAndOrderFilter pageAndOrder)
        {
            Query.Filters = ExpenseManagerMapper.Map<List<IFilterModel<BadgeModel>>>(filters);
            Query.PageAndOrderModelFilterModel = ExpenseManagerMapper.Map<PageAndOrderModelFilterModel<BadgeModel>>(pageAndOrder);
            return GetList().ToList();
        }

        /// <summary>
        /// Check all accounts if they dont deserve some badges
        /// </summary>
        public void CheckBadgesRequirements()
        {
            throw new NotImplementedException();
        }
    }
}
