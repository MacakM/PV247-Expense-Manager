using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    /// <summary>
    /// Service handles cost info entity operations
    /// </summary>
    public interface ICostInfoService : IService
    {
        /// <summary>
        /// Creates new cost info object in databse
        /// </summary>
        /// <param name="costInfo"></param>
        void CreateCostInfo(CostInfo costInfo);
        /// <summary>
        /// Updates existing cost info
        /// </summary>
        /// <param name="updatedCostInfo">updated cost info</param>
        void UpdateCostInfo(CostInfo updatedCostInfo);
        /// <summary>
        /// Deletes cost info specified by cost info
        /// </summary>
        /// <param name="costInfoId"></param>
        void DeleteCostInfo(int costInfoId);
        /// <summary>
        /// Get cost info specified by unique id
        /// </summary>
        /// <param name="costInfoId">Unique id</param>
        /// <returns>Cost info</returns>
        CostInfo GetCostInfo(int costInfoId);
        /// <summary>
        /// List cost types based on filter
        /// </summary>
        /// <param name="filter">Filters cost infos</param>
        /// <returns>List of cost infos</returns>
        List<CostInfo> ListCostInfos(CostInfoFilter filter);
        /// <summary>
        /// Recompute periodic costs and make them as new cost infos
        /// </summary>
        void RecomputePeriodicCosts();
    }
}
