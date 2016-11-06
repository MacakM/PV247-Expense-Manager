using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Services.Interfaces;

namespace ExpenseManager.Business.Facades
{
    /// <summary>
    /// 
    /// </summary>
    public class BalanceFacade
    {
        private readonly IAccountBadgeService _accountBadgeService;
        private readonly IBadgeService _badgeService;
        private readonly ICostInfoService _costInfoService;
        private readonly ICostTypeService _costTypeService;
        private readonly IPlanService _planService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountBadgeService"></param>
        /// <param name="badgeService"></param>
        /// <param name="costInfoService"></param>
        /// <param name="costTypeService"></param>
        /// <param name="planService"></param>
        public BalanceFacade(IAccountBadgeService accountBadgeService, IBadgeService badgeService, ICostInfoService costInfoService, ICostTypeService costTypeService, IPlanService planService)
        {
            _accountBadgeService = accountBadgeService;
            _badgeService = badgeService;
            _costInfoService = costInfoService;
            _costTypeService = costTypeService;
            _planService = planService;
        }
        
        #region Business operations
        /// <summary>
        /// 
        /// </summary>
        public void CheckAllPlansFulfilment()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        public void CheckBadgesRequirements()
        {
            
        }
        #endregion
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
        #region CostType CRUD
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costType"></param>
        public void CreateItemType(CostType costType)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costTypeId"></param>
        public void DeleteItemType(int costTypeId)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedCostType"></param>
        public void UpdateItemType(CostType updatedCostType)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemTypeId"></param>
        /// <returns></returns>
        public CostType GetItemType(int itemTypeId)
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<CostType> ListItemTypes(CostTypeFilter filter)
        {
            return null;
        }
        #endregion
        #region Badge CRUD
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badge"></param>
        public void CreateBadge(Badge badge)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badgeId"></param>
        public void DeleteBadge(int badgeId)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedBadge"></param>
        public void EditBadge(Badge updatedBadge)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badgeId"></param>
        /// <returns></returns>
        public Badge GetBadge(int badgeId)
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Badge> ListBages(BadgeFilter filter)
        {
            return null;
        }
        #endregion
        #region AccountBadge CRUD
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountBadge"></param>
        public void CreateAccountBadge(AccountBadge accountBadge)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountBadgeId"></param>
        public void DeleteAccountBadge(int accountBadgeId)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedAccountBadge"></param>
        public void UpdateAccountBadge(AccountBadge updatedAccountBadge)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountBadgeId"></param>
        /// <returns></returns>
        public AccountBadge GetAccountBadge(int accountBadgeId)
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<AccountBadge> ListAccountBadges(AccountBadgeFilter filter)
        {
            return null;
        } 
        #endregion
    }
}
