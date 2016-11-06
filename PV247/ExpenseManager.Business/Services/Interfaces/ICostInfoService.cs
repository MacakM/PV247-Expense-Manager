using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICostInfoService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costInfo"></param>
        void CreateCostInfo(CostInfo costInfo);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedCostInfo"></param>
        void UpdateCostInfo(CostInfo updatedCostInfo);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costInfoId"></param>
        void DeleteCostInfo(int costInfoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costInfoId"></param>
        /// <returns></returns>
        CostInfo GetCostInfo(int costInfoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<CostInfo> ListCostInfos(CostInfoFilter filter);
        /// <summary>
        /// 
        /// </summary>
        void RecomputePeriodicCosts();
    }
}
