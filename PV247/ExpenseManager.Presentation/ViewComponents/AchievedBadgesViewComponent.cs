using System.Collections.Generic;
using AutoMapper;
using ExpenseManager.Business.Facades;
using ExpenseManager.Presentation.Authentication;
using ExpenseManager.Presentation.Models.Achievement;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.ViewComponents
{
    /// <summary>
    /// View component for displaying achieved badges in right menu
    /// </summary>
    public class AchievedBadgesViewComponent : ViewComponent
    {
        private readonly BalanceFacade _balanceFacade;
        private readonly ICurrentAccountProvider _currentAccountProvider;
        private readonly IRuntimeMapper _mapper;

        private const int MaxNumberOfBadgeRows = 5;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="balanceFacade"></param>
        /// <param name="currentAccountProvider"></param>
        /// <param name="mapper"></param>
        public AchievedBadgesViewComponent(
            BalanceFacade balanceFacade, 
            ICurrentAccountProvider currentAccountProvider,
            Mapper mapper)
        {
            _balanceFacade = balanceFacade;
            _currentAccountProvider = currentAccountProvider;
            _mapper = mapper.DefaultContext.Mapper;
        }

        /// <summary>
        /// Invokes view component
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            var account = _currentAccountProvider.GetCurrentAccount(HttpContext.User);
            var badges = _balanceFacade.ListAchievedAccountBadges(account.Id, MaxNumberOfBadgeRows);
            var model = _mapper.Map<List<AccountBadgeViewModel>>(badges);
            return View(model);
        }
    }
}
