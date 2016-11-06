using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPlanService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plan"></param>
        void CreatePlan(Plan plan);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plan"></param>
        void UpdatePlan(Plan plan);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="planId"></param>
        void DeletePlan(int planId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        Plan GetPlan(int planId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<Plan> ListPlans(PlanFilter filter);
        /// <summary>
        /// 
        /// </summary>
        void CheckAllPlansFulfillment();
    }
}
