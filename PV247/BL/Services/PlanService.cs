using System.Collections.Generic;
using BL.DTOs;
using BL.Infrastructure.Mapping;
using BL.Infrastructure.Services;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Services
{
    public class PlanAndCrudService : ExpenseManagerQueryAndCrudServiceBase<Plan, int, IList<PlanDTO>, PlanDTO>
    {
        public PlanAndCrudService(IQuery<IList<PlanDTO>> query, IRepository<Plan, int> repository, IEntityDTOMapper<Plan, PlanDTO> mapper) : base(query, repository, mapper)
        {
        }

        public void CreatePlan(PlanDTO planDTO)
        {
            Save(planDTO);
        }

        // TODO add more functionality
    }
}
