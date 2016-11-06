using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICostTypeService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costType"></param>
        void CreateCostType(CostType costType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costType"></param>
        void UpdateCostType(CostType costType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costTypeId"></param>
        void DeleteCostType(int costTypeId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costTypeId"></param>
        /// <returns></returns>
        CostType GetCostType(int costTypeId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<CostType> ListCostTypes(CostTypeFilter filter);
    }
}
