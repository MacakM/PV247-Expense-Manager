using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using ExpenseManager.Business.DTOs;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    public class PlanAndCrudService : ExpenseManagerQueryAndCrudServiceBase<Plan, int, IList<PlanDTO>, PlanDTO>, IPlanAndCrudService
    {
        private readonly PlanRepository _planRepository;

        public PlanAndCrudService(IQuery<IList<PlanDTO>> query, ExpenseManagerRepository<Plan, int> repository,
            Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider, PlanRepository planRepository)
            : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
            _planRepository = planRepository;
        }

        public void CreatePlan(PlanDTO planDto)
        {
            Save(planDto);
        }

        public void EditPlan(PlanDTO planDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var plan = _planRepository.GetById(planDto.Id, "Account", "CostType");
                Mapper.Map(planDto, plan);
                _planRepository.Update(plan);
                uow.Commit();
            }
        }

        public void DeletePlan(int planId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                _planRepository.Delete(planId);
                uow.Commit();
            }
        }

        public PlanDTO GetPlan(int planId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var plan = _planRepository.GetById(planId);
                return plan != null ? Mapper.Map<PlanDTO>(plan) : null;
            }
        }

        public IEnumerable<PlanDTO> ListPlans(PlanFilter filter, int requiredPage = 1)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<PlanDTO, object>>[] EntityIncludes => new Expression<Func<PlanDTO, object>>[]
        {
            
        };
    }
}
