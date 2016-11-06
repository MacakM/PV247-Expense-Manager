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
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.Filters;

namespace ExpenseManager.Business.Services.Implementations
{
    // TODO doc
    /// <summary>
    /// 
    /// </summary>
    public class BadgeService : ExpenseManagerQueryAndCrudServiceBase<BadgeModel, int, ListBadgesQuery, Badge, BadgeModelFilter>, IBadgeService
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
        public BadgeService(ListBadgesQuery query, ExpenseManagerRepository<BadgeModel, int> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badge"></param>
        public void CreateBadge(Badge badge)
        {
            Save(badge);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badgeEdited"></param>
        public void UpdateBadge(Badge badgeEdited)
        {
            Save(badgeEdited);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badgeId"></param>
        public void DeleteBadge(int badgeId)
        {
            Delete(badgeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badgeId"></param>
        /// <returns></returns>
        public Badge GetBadge(int badgeId)
        {
            return GetDetail(badgeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Badge> ListBadges(BadgeFilter filter)
        {
            Query.Filter = ExpenseManagerMapper.Map<BadgeModelFilter>(filter);
            return GetList().ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badge"></param>
        /// <param name="account"></param>
        public void AchieveBadge(Badge badge, Account account)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        public void CheckBadgesRequirements()
        {
            throw new NotImplementedException();
        }
    }
}
