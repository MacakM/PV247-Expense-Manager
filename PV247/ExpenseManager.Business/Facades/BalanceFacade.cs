using System;
using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Services.Interfaces;

namespace ExpenseManager.Business.Facades
{
    /// <summary>
    /// Handles operations over account balance and related entities
    /// </summary>
    public class BalanceFacade
    {
        private readonly IAccountBadgeService _accountBadgeService;

        private readonly IBadgeService _badgeService;

        private readonly ICostInfoService _costInfoService;

        private readonly ICostTypeService _costTypeService;

        private readonly IPlanService _planService;

        private readonly IBadgeManagerService _badgeManagerService;

        /// <summary>
        /// Balance facade construtor
        /// </summary>
        /// <param name="accountBadgeService">Account Badge service</param>
        /// <param name="badgeService">Badge service</param>
        /// <param name="costInfoService">Cost info service</param>
        /// <param name="costTypeService">Cost type service</param>
        /// <param name="planService">Plan service</param>
        /// <param name="badgeManagerService">Badge manager service</param>
        public BalanceFacade(IAccountBadgeService accountBadgeService, IBadgeService badgeService, ICostInfoService costInfoService, ICostTypeService costTypeService, IPlanService planService, IBadgeManagerService badgeManagerService)
        {
            _accountBadgeService = accountBadgeService;
            _badgeService = badgeService;
            _costInfoService = costInfoService;
            _costTypeService = costTypeService;
            _planService = planService;
            _badgeManagerService = badgeManagerService;
        }

        #region Business operations
        /// <summary>
        /// Lists all plans that can be closed by user - MUST BE PLANTYPE.SAVE
        /// </summary>
        public List<Plan> ListAllCloseablePlans(Guid accountId)
        {
            return _planService.ListAllCloseablePlans(accountId, GetBalance(accountId));
        }

        /// <summary>
        /// Lists all plans that are in progress for current user
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<Plan> ListPlansInProgress(Guid accountId)
        {
            return _planService.ListPlansInProgress(accountId);
        }

        /// <summary>
        /// Check all MaxSpend plans and in they at deadline and accomplished set em as completed
        /// </summary>
        /// <returns></returns>
        public void CheckAllMaxSpendDeadlines()
        {
             _planService.CheckAllMaxSpendDeadlines();
        }

        /// <summary>
        /// Returns current balance of account
        /// </summary>
        public decimal GetBalance(Guid accountId)
        {
            return _costInfoService.GetBalance(accountId);
        }

        /// <summary>
        /// Plan is marked as closed and is transfered into database as CostInfo - user spent m
        /// </summary>
        /// <param name="plan"></param>
        public void ClosePlan(Plan plan)
        {
            _planService.ClosePlan(plan);
        }

        /// <summary>
        /// Checks all account if they can get badge
        /// </summary>
        public void CheckBadgesRequirements()
        {
            _badgeManagerService.CheckBadgesRequirements();
        }

        /// <summary>
        /// Recomputes periodic costs, should be called every day in order to refresh all costs. Smallest period was set as one day
        /// </summary>
        public void RecomputePeriodicCosts()
        {
            _costInfoService.RecomputePeriodicCosts();
        }

        #endregion
        #region CostInfo CRUD
        /// <summary>
        /// Creates new cost info object in databse
        /// </summary>
        /// <param name="costInfo"></param>
        public Guid CreateItem(CostInfo costInfo)
        {
            return _costInfoService.CreateCostInfo(costInfo);
        }

        /// <summary>
        /// Deletes cost info specified by cost info
        /// </summary>
        /// <param name="costInfoId"></param>
        public void DeleteItem(Guid costInfoId)
        {
            _costInfoService.DeleteCostInfo(costInfoId);
        }

        /// <summary>
        /// Updates existing cost info
        /// </summary>
        /// <param name="updatedCostInfo">updated cost info</param>
        public void UpdateItem(CostInfo updatedCostInfo)
        {
            _costInfoService.UpdateCostInfo(updatedCostInfo);
        }

        /// <summary>
        /// Get cost info specified by unique id
        /// </summary>
        /// <param name="costInfoId">Unique id</param>
        /// <returns>Cost info</returns>
        public CostInfo GetItem(Guid costInfoId)
        {
            return _costInfoService.GetCostInfo(costInfoId);
        }

        /// <summary>
        /// List cost types based on filter
        /// </summary>
        /// <param name="filter">Filters cost infos</param>
        /// <returns>List of cost infos</returns>
        public List<CostInfo> ListItems(CostInfoFilter filter)
        {
            return _costInfoService.ListCostInfos(filter);
        }

        /// <summary>
        /// Gets the count of rows in database filtered by filter
        /// Used for pagination
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int GetCostInfosCount(CostInfoFilter filter)
        {
            return _costInfoService.GetCostInfosCount(filter);
        }

        #endregion
        #region Plan CRUD
        /// <summary>
        /// Creates new plan in databse
        /// </summary>
        /// <param name="plan">Object to be saved to database</param>
        public Guid CreatePlan(Plan plan)
        {
            return _planService.CreatePlan(plan);
        }

        /// <summary>
        /// Deletes plen with specified id
        /// </summary>
        /// <param name="planId">Unique id of deleted plan</param>
        public void DeletePlan(Guid planId)
        {
            _planService.DeletePlan(planId);
        }
        /// <summary>
        /// Updates plan, must have id of updated plan!
        /// </summary>
        /// <param name="updatedPlan">Plan object with id of existing plan</param>
        public void UpdatePlan(Plan updatedPlan)
        {
            _planService.UpdatePlan(updatedPlan);
        }

        /// <summary>
        /// Get specific plan specified by unique id
        /// </summary>
        /// <param name="planId">Unique id of plan</param>
        /// <returns></returns>
        public Plan GetPlan(Guid planId)
        {
            return _planService.GetPlan(planId);
        }

        /// <summary>
        /// Lists all plans that match filters criterias
        /// </summary>
        /// <param name="filter">Filters plans</param>
        /// <returns></returns>
        public List<Plan> ListPlans(PlanFilter filter)
        {
            return _planService.ListPlans(filter);
        }

        #endregion
        #region CostType CRUD
        /// <summary>
        /// Creaates new cost type
        /// </summary>
        /// <param name="costType">Object to be added to database</param>
        public Guid CreateItemType(CostType costType)
        {
            return _costTypeService.CreateCostType(costType);
        }

        /// <summary>
        /// Deletes cost type specified by id
        /// </summary>
        /// <param name="costTypeId">Unique cost type id</param>
        public void DeleteItemType(Guid costTypeId)
        {
            _costTypeService.DeleteCostType(costTypeId);
        }

        /// <summary>
        /// Updates existing cost type
        /// </summary>
        /// <param name="updatedCostType">Modified existing cost type</param>
        public void UpdateItemType(CostType updatedCostType)
        {
            _costTypeService.UpdateCostType(updatedCostType);
        }

        /// <summary>
        /// Get cost type specified by unique id
        /// </summary>
        /// <param name="itemTypeId">Unique cost type id</param>
        /// <returns></returns>
        public CostType GetItemType(Guid itemTypeId)
        {
            return _costTypeService.GetCostType(itemTypeId);
        }

        /// <summary>
        /// List cost types specified by filter
        /// </summary>
        /// <param name="filter">Filters cost types</param>
        /// <returns>List of cost typer</returns>
        public List<CostType> ListItemTypes(CostTypeFilter filter)
        {
            return _costTypeService.ListCostTypes(filter);
        }

        #endregion
        #region Badge CRUD
        /// <summary>
        /// Creates new Badge object in database
        /// </summary>
        /// <param name="badge">new Badge</param>
        public Guid CreateBadge(Badge badge)
        {
            return _badgeService.CreateBadge(badge);
        }

        /// <summary>
        /// Deletes badge cpecified by id
        /// </summary>
        /// <param name="badgeId"></param>
        public void DeleteBadge(Guid badgeId)
        {
            _badgeService.DeleteBadge(badgeId);
        }

        /// <summary>
        /// Updates existing badge in database
        /// </summary>
        /// <param name="updatedBadge"></param>
        public void UpdateBadge(Badge updatedBadge)
        {
            _badgeService.UpdateBadge(updatedBadge);
        }

        /// <summary>
        /// Get specific badge by unique id
        /// </summary>
        /// <param name="badgeId"></param>
        /// <returns></returns>
        public Badge GetBadge(Guid badgeId)
        {
            return _badgeService.GetBadge(badgeId);
        }

        /// <summary>
        /// Lists filtered badges
        /// </summary>
        /// <param name="filter">Filters badges</param>
        /// <returns></returns>
        public List<Badge> ListBadges(BadgeFilter filter)
        {
            return _badgeService.ListBadges(filter);
        }

        #endregion
        #region AccountBadge CRUD
        /// <summary>
        /// Add new badge to account by creating new AccountBadge object in database
        /// </summary>
        /// <param name="accountBadge"></param>
        public Guid CreateAccountBadge(AccountBadge accountBadge)
        {
            return _accountBadgeService.CreateAccountBadge(accountBadge);
        }

        /// <summary>
        /// Deletes specified account badge
        /// </summary>
        /// <param name="accountBadgeId"></param>
        public void DeleteAccountBadge(Guid accountBadgeId)
        {
            _accountBadgeService.DeleteAccountBadge(accountBadgeId);
        }

        /// <summary>
        /// Updates existing account badge
        /// </summary>
        /// <param name="updatedAccountBadge"></param>
        public void UpdateAccountBadge(AccountBadge updatedAccountBadge)
        {
            _accountBadgeService.UpdateAccountBadge(updatedAccountBadge);
        }

        /// <summary>
        /// Get account badge specified by id
        /// </summary>
        /// <param name="accountBadgeId"></param>
        /// <returns></returns>
        public AccountBadge GetAccountBadge(Guid accountBadgeId)
        {
            return _accountBadgeService.GetAccountBadge(accountBadgeId);
        }

        /// <summary>
        /// List filtered account badges
        /// </summary>
        /// <param name="filter">Filters account badgess</param>
        /// <returns></returns>
        public List<AccountBadge> ListAccountBadges(AccountBadgeFilter filter)
        {
            return _accountBadgeService.ListAccountBadges(filter);
        } 
        #endregion
    }
}
