using System;
using System.Collections.Generic;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    public class PlanService : ExpenseManagerQueryAndCrudServiceBase<PlanModel, int, IList<Plan>, Plan>, IPlanService
    {
        private readonly PlanRepository _planRepository;

        public PlanService(IQuery<IList<Plan>> query, ExpenseManagerRepository<PlanModel, int> repository,
            Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider, PlanRepository planRepository)
            : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
            _planRepository = planRepository;
        }

        public void CreatePlan(Plan plan)
        {
            Save(plan);
        }

        public void EditPlan(Plan planEdited)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var plan = _planRepository.GetById(planEdited.Id, EntityIncludes);
                Mapper.Map(plan, plan);
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

        public Plan GetPlan(int planId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var plan = _planRepository.GetById(planId);
                return plan != null ? Mapper.Map<Plan>(plan) : null;
            }
        }

        public IEnumerable<Plan> ListPlans(PlanFilter filter)
        {
            throw new NotImplementedException();
        }

        protected override string[] EntityIncludes { get; } =
        {
            nameof(PlanModel.Account),
            nameof(PlanModel.PlannedType)
        };
    }
}
