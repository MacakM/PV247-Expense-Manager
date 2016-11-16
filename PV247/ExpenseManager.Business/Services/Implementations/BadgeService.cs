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
    public class BadgeService : ExpenseManagerQueryAndCrudServiceBase<BadgeModel, int, Badge>, IBadgeService
    {
        /// <summary>
        /// 
        /// </summary>
        protected override string[] EntityIncludes { get; } =
       {
            nameof(BadgeModel.Accounts)
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="repository"></param>
        /// <param name="expenseManagerMapper"></param>
        /// <param name="unitOfWorkProvider"></param>
        public BadgeService(ExpenseManagerQuery<BadgeModel> query, ExpenseManagerRepository<BadgeModel, int> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
        }
        /// <summary>
        /// Creates new Badge object in database
        /// </summary>
        public void CreateBadge(Badge badge)
        {
            Save(badge);
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
        public void DeleteBadge(int badgeId)
        {
            Delete(badgeId);
        }
        /// <summary>
        /// Get specific badge by unique id
        /// </summary>
        /// <param name="badgeId"></param>
        /// <returns></returns>
        public Badge GetBadge(int badgeId)
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
        /// Check all accounts if they dont deserve some badges
        /// </summary>
        public void CheckBadgesRequirements()
        {
            throw new NotImplementedException();
        }
    }
}
