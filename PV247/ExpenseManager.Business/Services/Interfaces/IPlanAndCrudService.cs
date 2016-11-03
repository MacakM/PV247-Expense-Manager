using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Business.DTOs;
using ExpenseManager.Database.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    public interface IPlanAndCrudService
    {
        void CreatePlan(PlanDTO planDto);
        void EditPlan(PlanDTO planDto);
        void DeletePlan(int planId);
        PlanDTO GetPlan(int planId);
        IEnumerable<PlanDTO> ListPlans(PlanFilter filter, int requiredPage = 1);
    }
}
