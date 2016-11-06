using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    public interface IPlanService
    {
        void CreatePlan(Plan plan);
        void UpdatePlan(Plan plan);
        void DeletePlan(int planId);
        Plan GetPlan(int planId);
        IEnumerable<Plan> ListPlans(PlanFilter filter);
    }
}
