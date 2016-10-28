using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using APILayer.DTOs;
using AutoMapper;
using BL.Infrastructure;
using DAL.Entities;
using DAL.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Services
{
    public class PlanAndCrudService : ExpenseManagerQueryAndCrudServiceBase<Plan, int, IList<PlanDTO>, PlanDTO>
    {
        public PlanAndCrudService(IQuery<IList<PlanDTO>> query, IRepository<Plan, PlanDTO, int> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider)
            : base(query, repository, expenseManagerMapper, unitOfWorkProvider) { }

        public void CreatePlan(PlanDTO planDTO)
        {
            Save(planDTO);
        }

        // TODO add more functionality


        protected override Expression<Func<PlanDTO, object>>[] EntityIncludes => new Expression<Func<PlanDTO, object>>[]
        {
            
        };
    }
}
