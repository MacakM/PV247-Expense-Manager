using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Facades
{
    /// <summary>
    /// 
    /// </summary>
    public class BalanceFacade
    {
        #region CostInfo CRUD
        /// <summary>
        /// 
        /// </summary>
        public void CreateItem(CostInfo costInfo)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costInfoId"></param>
        public void DeleteItem(int costInfoId)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedCostInfo"></param>
        public void UpdateItem(CostInfo updatedCostInfo)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costInfoId"></param>
        /// <returns></returns>
        public CostInfo GetItem(int costInfoId)
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<CostInfo> ListItem(CostInfoFilter filter)
        {
            return null;
        }
        #endregion
        #region Plan CRUD
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plan"></param>
        public void CreatePlan(Plan plan)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="planId"></param>
        public void DeletePlan(int planId)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedPlan"></param>
        public void UpdatePlan(Plan updatedPlan)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public Plan GetPlan(int planId)
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Plan> ListPlans(PlanFilter filter)
        {
            return null;
        }
        #endregion


    }
}
