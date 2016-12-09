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
        
        private readonly IPlanService _planService = BusinessLayerDIManager.Resolve<IPlanService>();

        private readonly IGraphService _graphService = BusinessLayerDIManager.Resolve<IGraphService>();

        private readonly IAccountBadgeService _accountBadgeService = BusinessLayerDIManager.Resolve<IAccountBadgeService>();
   
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
            var filters = FilterFactory.GetPlanFilters(accountId, null, null, null, null);
            return _planService.ListPlans(filters, FilterFactory.GetPageAndOrderable<PlanModel>(pageInfo));
        }

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
    }
}
