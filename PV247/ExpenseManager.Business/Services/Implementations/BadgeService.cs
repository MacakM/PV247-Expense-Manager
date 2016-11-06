using System;
using System.Collections.Generic;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;
using ExpenseManager.Business.Services.Interfaces;

namespace ExpenseManager.Business.Services.Implementations
{
    public class BadgeService : ExpenseManagerQueryAndCrudServiceBase<BadgeModel, int, IList<Badge>, Badge>, IBadgeService
    {
        private readonly BadgeRepository _badgeRepository;

        public BadgeService(IQuery<IList<Badge>> query, ExpenseManagerRepository<BadgeModel, int> repository,
            Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider, BadgeRepository badgeRepository)
            : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
            _badgeRepository = badgeRepository;
        }

        public void CreateBadge(Badge badge)
        {
            Save(badge);
        }

        public void UpdateBadge(Badge badgeEdited)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var badge = _badgeRepository.GetById(badgeEdited.Id, EntityIncludes);
                Mapper.Map(badge, badge);
                _badgeRepository.Update(badge);
                uow.Commit();
            }
        }

        public void DeleteBadge(int badgeId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                _badgeRepository.Delete(badgeId);
                uow.Commit();
            }
        }

        public Badge GetBadge(int badgeId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var plan = _badgeRepository.GetById(badgeId);
                return plan != null ? Mapper.Map<Badge>(plan) : null;
            }
        }

        public List<Badge> ListBadges(BadgeFilter filter)
        {
            throw new NotImplementedException();
        }

        public void AchieveBadge(Badge badge, Account account)
        {
            throw new NotImplementedException();
        }

        protected override string[] EntityIncludes { get; } =
        {
            nameof(BadgeModel.Accounts)
        };
    }
}
