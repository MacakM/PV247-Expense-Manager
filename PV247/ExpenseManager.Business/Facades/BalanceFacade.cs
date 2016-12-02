using System;
using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.DataTransferObjects.Factories;
using ExpenseManager.Business.Infrastructure.CastleWindsor;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.Facades
{
    /// <summary>
    /// Handles operations over account balance and related entities
    /// </summary>
    public class BalanceFacade
    {
        private readonly IBadgeService _badgeService = BusinessLayerDIManager.Resolve<IBadgeService>();

        private readonly ICostInfoService _costInfoService = BusinessLayerDIManager.Resolve<ICostInfoService>();

        private readonly ICostTypeService _costTypeService = BusinessLayerDIManager.Resolve<ICostTypeService>();

        private readonly IPlanService _planService = BusinessLayerDIManager.Resolve<IPlanService>();

        private readonly IGraphService _graphService = BusinessLayerDIManager.Resolve<IGraphService>();

        private readonly IAccountBadgeService _accountBadgeService = BusinessLayerDIManager.Resolve<IAccountBadgeService>();

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
            _badgeService.CheckBadgesRequirements();
        }

        /// <summary>
        /// Recomputes periodic costs, should be called every day in order to refresh all costs. Smallest period was set as one day
        /// </summary>
        public void RecomputePeriodicCosts()
        {
            _costInfoService.RecomputePeriodicCosts();
        }

        /// <summary>
        /// Gets data for graph in form of daily balance
        /// </summary>
        public List<DayTotalBalance> GetDailyBalanceGraphData(Guid accountId)
        {
            var totalBalance = _costInfoService.GetBalance(accountId);
            return _graphService.GetTotalDailyBalanceGraphData(accountId, totalBalance);
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
        /// List cost infos
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<CostInfo> ListItems(Guid typeId, PageInfo pageInfo)
        {
            return ListItems(null, null, null, null, null, null, typeId, null, pageInfo);
        }

        /// <summary>
        /// List cost infos
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="periodicity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<CostInfo> ListItems(Guid? accountId, Periodicity? periodicity,  PageInfo pageInfo)
        {
            return ListItems(accountId, periodicity, null, null, null, null, null, null, pageInfo);
        }

        /// <summary>
        /// List cost types based on filter
        /// </summary>
        /// <param name="periodicity"></param>
        /// <param name="isIncome"></param>
        /// <param name="accountId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="moneyFrom"></param>
        /// <param name="moneyTo"></param>
        /// <param name="costTypeId"></param>
        /// <param name="pageInfo"></param>
        /// <returns>List of cost infos</returns>
        public List<CostInfo> ListItems(Guid? accountId, Periodicity? periodicity, DateTime? dateFrom, DateTime? dateTo, decimal? moneyFrom, decimal? moneyTo, Guid? costTypeId, bool? isIncome, PageInfo pageInfo)
        {
            var filters = FilterFactory.GetCostItemsFilters(accountId, periodicity, dateFrom, dateTo, moneyFrom, moneyTo, costTypeId,isIncome);
            return _costInfoService.ListCostInfos(filters, FilterFactory.GetPageAndOrderable<CostInfoModel>(pageInfo));
        }

        /// <summary>
        /// Gets the count of rows in database filtered by filter
        /// Used for pagination
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="periodicity"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="moneyFrom"></param>
        /// <param name="moneyTo"></param>
        /// <param name="costTypeId"></param>
        /// <param name="isIncome"></param>
        /// <returns></returns>
        public int GetCostInfosCount(Guid? accountId, Periodicity? periodicity, DateTime? dateFrom, DateTime? dateTo, decimal? moneyFrom, decimal? moneyTo, Guid? costTypeId, bool? isIncome)
        {
            var filters = FilterFactory.GetCostItemsFilters(accountId, periodicity, dateFrom, dateTo, moneyFrom, moneyTo, costTypeId, isIncome);
            return _costInfoService.GetCostInfosCount(filters, null);
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
        /// <param name="accountId"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<Plan> ListPlans(Guid? accountId, PageInfo pageInfo)
        {
            var filters = FilterFactory.GetPlanFilters(accountId);
            return _planService.ListPlans(filters, FilterFactory.GetPageAndOrderable<PlanModel>(pageInfo));
        }

        #endregion
        #region CostType CRUD
        /// <summary>
        /// Creaates new cost type
        /// </summary>
        /// <param name="costType">Object to be added to database</param>
        public void CreateItemType(CostType costType)
        {
            _costTypeService.CreateCostType(costType);
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
        /// Lists all cost types for given account id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<CostType> ListItemTypes(Guid accountId)
        {
            var filters = FilterFactory.GetCostTypeFilters(accountId);
            return _costTypeService.ListCostTypes(filters, null);
        }

        /// <summary>
        /// List cost types specified by filter
        /// </summary>
        /// <param name="costTypeName"></param>
        /// <param name="pageInfo"></param>
        /// <returns>List of cost typer</returns>
        public List<CostType> ListItemTypes(string costTypeName, PageInfo pageInfo)
        {
            var filters = FilterFactory.GetCostTypeFilters(costTypeName);
            return _costTypeService.ListCostTypes(filters, FilterFactory.GetPageAndOrderable<CostTypeModel>(pageInfo));
        }

        #endregion
        #region Badge CRUD
        /// <summary>
        /// Creates new Badge object in database
        /// </summary>
        /// <param name="badge">new Badge</param>
        public void CreateBadge(Badge badge)
        {
            _badgeService.CreateBadge(badge);
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
        /// <param name="badgeName"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<Badge> ListBadges(string badgeName, PageInfo pageInfo)
        {
            var filters = FilterFactory.GetBadgeFilters(badgeName);
            return _badgeService.ListBadges(filters, FilterFactory.GetPageAndOrderable<BadgeModel>(pageInfo));
        }

        /// <summary>
        /// Lists all achieved badges for given accountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="ammount"></param>
        /// <returns></returns>
        public List<AccountBadge> ListAchievedAccountBadges(Guid accountId, int? ammount = null)
        {
            var filters = FilterFactory.GetAccountBadgeFilters(accountId);
            
            var pageInfo = new PageInfo()
            {
                OrderByDesc = true,
                OrderByPropertyName = nameof(AccountBadgeModel.Achieved),
            };
            if (ammount != null)
            {
                pageInfo.PageNumber = 1;
                pageInfo.PageSize = ammount.Value;
            }

            var pageFilter = FilterFactory.GetPageAndOrderable<AccountBadgeModel>(pageInfo);
            
            return _accountBadgeService.ListAccountBadges(filters, pageFilter);
        }

        /// <summary>
        /// Lists all yet not achieved badges for given accountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<Badge> ListNotAchievedBadges(Guid accountId)
        {
            return _badgeService.ListNotAchievedBadges(accountId);
        }

       
        #endregion


    }
}
