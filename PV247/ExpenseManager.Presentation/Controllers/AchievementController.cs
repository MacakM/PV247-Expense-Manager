using System.Collections.Generic;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.Facades;
using ExpenseManager.Presentation.Authentication;
using ExpenseManager.Presentation.Models.Achievement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers
{
    /// <summary>
    /// Achievement controller
    /// </summary>
    [Authorize]
    [Authorize(Policy = "HasAccount")]
    public class AchievementController : BaseController
    {
        private readonly BalanceFacade _balanceFacade;

        /// <summary>
        /// Constructor
        /// </summary>
        public AchievementController(ICurrentAccountProvider currentAccountProvider, Mapper mapper, BalanceFacade balanceFacade) : base(currentAccountProvider, mapper)
        {
            _balanceFacade = balanceFacade;
        }

        /// <summary>
        /// Index page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var account = CurrentAccountProvider.GetCurrentAccount(HttpContext.User);
            var model = new IndexViewModel()
            {
                AchievedBadges = GetAchievedBadges(account),
                NotAchievedBadges = GetNotAchievedBadges(account)
            };
            return View(model);
        }

        private List<BadgeViewModel> GetNotAchievedBadges(Account account)
        {
            var badges = _balanceFacade.ListNotAchievedBadges(account.Id);
            return Mapper.Map<List<BadgeViewModel>>(badges);
        }

        private List<AccountBadgeViewModel> GetAchievedBadges(Account account)
        {
            var badges = _balanceFacade.ListAchievedAccountBadges(account.Id);
            return Mapper.Map<List<AccountBadgeViewModel>>(badges);
        }
    }
}