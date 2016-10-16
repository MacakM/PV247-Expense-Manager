using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs;
using BL.Infrastructure;
using BL.Infrastructure.Mapping;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Services
{
    public class PlanService : ExpenseManagerCrudServiceBase<Plan, int, IList<PlanDTO>, PlanDTO>
    {
        public PlanService(IQuery<IList<PlanDTO>> query, IRepository<Plan, int> repository, IEntityDTOMapper<Plan, PlanDTO> mapper) : base(query, repository, mapper)
        {
        }

        public void CreatePlan()
        {

        }

        
    }
}
