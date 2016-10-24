using System.Collections.Generic;
using AutoMapper;
using BL.DTOs;
using BL.Infrastructure.Services;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Services
{
    public class PlanAndCrudService : ExpenseManagerQueryAndCrudServiceBase<Plan, int, IList<PlanDTO>, PlanDTO>
    {
        public PlanAndCrudService(IQuery<IList<PlanDTO>> query, IRepository<Plan, int> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider)
            : base(query, repository, expenseManagerMapper, unitOfWorkProvider) { }

        public void CreatePlan(PlanDTO planDTO)
        {
            Save(planDTO);
        }

        // TODO add more functionality
    }
}
