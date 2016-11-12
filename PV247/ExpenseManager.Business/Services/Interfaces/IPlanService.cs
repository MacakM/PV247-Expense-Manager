using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    /// <summary>
    /// Service handles plan entity operations
    /// </summary>
    public interface IPlanService
    {
        /// <summary>
        /// Creates new plan in databse
        /// </summary>
        /// <param name="plan">Object to be saved to database</param>
        void CreatePlan(Plan plan);
        /// <summary>
        /// Updates plan, must have id of updated plan!
        /// </summary>
        /// <param name="plan">Plan object with id of existing plan</param>
        void UpdatePlan(Plan plan);
        /// <summary>
        /// Deletes plen with specified id
        /// </summary>
        /// <param name="planId">Unique id of deleted plan</param>
        void DeletePlan(int planId);
        /// <summary>
        /// Get specific plan specified by unique id
        /// </summary>
        /// <param name="planId">Unique id of plan</param>
        /// <returns></returns>
        Plan GetPlan(int planId);
        /// <summary>
        /// Lists all plans that match filters criterias
        /// </summary>
        /// <param name="filter">Filters plans</param>
        /// <returns></returns>
        List<Plan> ListPlans(PlanFilter filter);
        /// <summary>
        /// Lists all plans, that can be closed by user
        /// </summary>
        /// <returns>List of plans</returns>
        List<Plan> ListAllCloseablePlans();
        /// <summary>
        /// Transfers plan into cost
        /// </summary>
        /// <param name="plan"></param>
        void ClosePlan(Plan plan);
    }
}
