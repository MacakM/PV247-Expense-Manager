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
        public void CheckAllPlansFulfillment()
        {
            _planService.CheckAllPlansFulfillment();
        }
        /// <summary>
        /// 
        /// </summary>
        public void CheckBadgesRequirements()
        {
            _badgeService.CheckBadgesRequirements();
        }
        /// <summary>
        /// 
        /// </summary>
        public void RecomputePeriodicCosts()
        {
            _costInfoService.RecomputePeriodicCosts();
        }
        #endregion
        #region CostInfo CRUD
        /// <summary>
        /// 
        /// </summary>
        public void CreateItem(CostInfo costInfo)
        {
            _costInfoService.CreateCostInfo(costInfo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costInfoId"></param>
        public void DeleteItem(int costInfoId)
        {
            _costInfoService.DeleteCostInfo(costInfoId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedCostInfo"></param>
        public void UpdateItem(CostInfo updatedCostInfo)
        {
            _costInfoService.UpdateCostInfo(updatedCostInfo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costInfoId"></param>
        /// <returns></returns>
        public CostInfo GetItem(int costInfoId)
        {
            return _costInfoService.GetCostInfo(costInfoId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<CostInfo> ListItem(CostInfoFilter filter)
        {
            return _costInfoService.ListCostInfos(filter);
        }
        #endregion
        #region Plan CRUD
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plan"></param>
        public void CreatePlan(Plan plan)
        {
            _planService.CreatePlan(plan);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="planId"></param>
        public void DeletePlan(int planId)
        {
            _planService.DeletePlan(planId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedPlan"></param>
        public void UpdatePlan(Plan updatedPlan)
        {
            _planService.UpdatePlan(updatedPlan);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public Plan GetPlan(int planId)
        {
            return _planService.GetPlan(planId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Plan> ListPlans(PlanFilter filter)
        {
            return ListPlans(filter);
        }
        #endregion
        #region CostType CRUD
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costType"></param>
        public void CreateItemType(CostType costType)
        {
            _costTypeService.CreateCostType(costType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costTypeId"></param>
        public void DeleteItemType(int costTypeId)
        {
            _costTypeService.DeleteCostType(costTypeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedCostType"></param>
        public void UpdateItemType(CostType updatedCostType)
        {
            _costTypeService.UpdateCostType(updatedCostType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemTypeId"></param>
        /// <returns></returns>
        public CostType GetItemType(int itemTypeId)
        {
            return _costTypeService.GetCostType(itemTypeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<CostType> ListItemTypes(CostTypeFilter filter)
        {
            return _costTypeService.ListCostTypes(filter);
        }
        #endregion
        #region Badge CRUD
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badge"></param>
        public void CreateBadge(Badge badge)
        {
            _badgeService.CreateBadge(badge);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badgeId"></param>
        public void DeleteBadge(int badgeId)
        {
            _badgeService.DeleteBadge(badgeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedBadge"></param>
        public void UpdateBadge(Badge updatedBadge)
        {
            _badgeService.UpdateBadge(updatedBadge);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badgeId"></param>
        /// <returns></returns>
        public Badge GetBadge(int badgeId)
        {
            return _badgeService.GetBadge(badgeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Badge> ListBages(BadgeFilter filter)
        {
            return _badgeService.ListBadges(filter);
        }
        #endregion
        #region AccountBadge CRUD
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountBadge"></param>
        public void CreateAccountBadge(AccountBadge accountBadge)
        {
            _accountBadgeService.CreateAccountBadge(accountBadge);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountBadgeId"></param>
        public void DeleteAccountBadge(int accountBadgeId)
        {
            _accountBadgeService.DeleteAccountBadge(accountBadgeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedAccountBadge"></param>
        public void UpdateAccountBadge(AccountBadge updatedAccountBadge)
        {
            _accountBadgeService.UpdateAccountBadge(updatedAccountBadge);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountBadgeId"></param>
        /// <returns></returns>
        public AccountBadge GetAccountBadge(int accountBadgeId)
        {
            return _accountBadgeService.GetAccountBadge(accountBadgeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<AccountBadge> ListAccountBadges(AccountBadgeFilter filter)
        {
            return _accountBadgeService.ListAccountBadges(filter);
        } 
        #endregion
    }
}
