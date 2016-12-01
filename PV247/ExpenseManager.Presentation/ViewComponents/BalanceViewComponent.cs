using ExpenseManager.Business.Facades;
using ExpenseManager.Presentation.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.ViewComponents
{
    /// <summary>
    /// Component for displaying current balance
    /// </summary>
    public class BalanceViewComponent : ViewComponent
    {
        private readonly BalanceFacade _balanceFacade;

        private readonly ICurrentAccountProvider _currentAccountProvider;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="balanceFacade"></param>
        /// <param name="currentAccountProvider"></param>
        public BalanceViewComponent(BalanceFacade balanceFacade, ICurrentAccountProvider currentAccountProvider)
        {
            _balanceFacade = balanceFacade;
            _currentAccountProvider = currentAccountProvider;
        }

        /// <summary>
        /// Invokes view component
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            var account = _currentAccountProvider.GetCurrentAccount(HttpContext.User);
            var balance = _balanceFacade.GetBalance(account.Id);
            return View(balance);
        }
    }
}
