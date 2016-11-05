using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Database.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    public interface IPlanService
    {
        void CreatePlan(Plan plan);
        void EditPlan(Plan plan);
        void DeletePlan(int planId);
        Plan GetPlan(int planId);
        IEnumerable<Plan> ListPlans(PlanModelFilter filter, int requiredPage = 1);
    }
}
