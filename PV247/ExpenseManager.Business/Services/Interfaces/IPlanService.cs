using System;
using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.Services.Interfaces
{
    /// <summary>
    /// Service handles plan entity operations
    /// </summary>
    internal interface IPlanService : IService
    {
        /// <summary>
        /// Creates new plan in databse
        /// </summary>
        /// <param name="plan">Object to be saved to database</param>
        Guid CreatePlan(Plan plan);

        /// <summary>
        /// Updates plan, must have id of updated plan!
        /// </summary>
        /// <param name="plan">Plan object with id of existing plan</param>
        void UpdatePlan(Plan plan);

        /// <summary>
        /// Deletes plen with specified id
        /// </summary>
        /// <param name="planId">Unique id of deleted plan</param>
        void DeletePlan(Guid planId);

        /// <summary>
        /// Get specific plan specified by unique id
        /// </summary>
        /// <param name="planId">Unique id of plan</param>
        /// <returns></returns>
        Plan GetPlan(Guid planId);

        /// <summary>
        /// Lists all plans that match filters criterias
        /// </summary>
        /// <param name="filters">Filters plans</param>
        /// <param name="pageAndOrder">Orders</param>
        /// <returns></returns>
        List<Plan> ListPlans(List<IFilter<PlanModel>> filters, IPageAndOrderable<PlanModel> pageAndOrder);

        /// <summary>
        /// List closeable plans of account
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="accountBalance"></param>
        /// <returns></returns>
        List<Plan> ListAllCloseablePlans(Guid accountId, decimal accountBalance);

        /// <summary>
        /// Transfers plan into cost
        /// </summary>
        /// <param name="plan"></param>
        void ClosePlan(Plan plan);

        /// <summary>
        /// Check all MaxSpent plans and in they at deadline and accomplished set em as completed
        /// </summary>
        /// <returns></returns>
        void CheckAllMaxSpendDeadlines();

        /// <summary>
        /// Lists all plans that are in progress for current user
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        List<Plan> ListPlansInProgress(Guid accountId);
    }
}
