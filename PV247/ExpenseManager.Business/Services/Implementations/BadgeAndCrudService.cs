using System;
using System.Collections.Generic;
using AutoMapper;
using ExpenseManager.Business.DTOs;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;
using ExpenseManager.Business.Services.Interfaces;

namespace ExpenseManager.Business.Services.Implementations
{
    class BadgeAndCrudService : ExpenseManagerQueryAndCrudServiceBase<Badge, int, IList<BadgeDTO>, BadgeDTO>, IBadgeAndCrudService
    {
        private readonly BadgeRepository _badgeRepository;

        public BadgeAndCrudService(IQuery<IList<BadgeDTO>> query, ExpenseManagerRepository<Badge, int> repository,
            Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider, BadgeRepository badgeRepository)
            : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
            _badgeRepository = badgeRepository;
        }

        public void CreateBadge(BadgeDTO badgeDto)
        {
            Save(badgeDto);
        }

        public void EditBadge(BadgeDTO badgeDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var badge = _badgeRepository.GetById(badgeDto.Id, EntityIncludes);
                Mapper.Map(badgeDto, badge);
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

        public BadgeDTO GetBadge(int badgeId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var plan = _badgeRepository.GetById(badgeId);
                return plan != null ? Mapper.Map<BadgeDTO>(plan) : null;
            }
        }

        public IEnumerable<BadgeDTO> ListBadges(BadgeFilter filter, int requiredPage = 1)
        {
            throw new NotImplementedException();
        }

        public void AchieveBadge(BadgeDTO badgeDto, Account account)
        {
            throw new NotImplementedException();
        }

        protected override string[] EntityIncludes { get; } =
        {
            nameof(Badge.Users)
        };
    }
}
