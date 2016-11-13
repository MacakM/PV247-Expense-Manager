using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Facades;
using ExpenseManager.Presentation.Authentication;
using ExpenseManager.Presentation.Models.Expense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers
{
    /// <summary>
    /// Controller for displaying account settings
    /// </summary>
    [Authorize]
    public class AccountSettingsController : Controller
    {
        private readonly BalanceFacade _balanceFacade;
        private readonly ICurrentAccountProvider _currentAccountProvider;
        private readonly AccountFacade _accountFacade;
        private readonly IRuntimeMapper _mapper;

        /// <summary>
        /// Constructor for AccountSettingsController
        /// </summary>
        /// <param name="balanceFacade"></param>
        /// <param name="currentAccountProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="accountFacade"></param>
        public AccountSettingsController(
            BalanceFacade balanceFacade, 
            ICurrentAccountProvider currentAccountProvider, 
            Mapper mapper, 
            AccountFacade accountFacade)
        {
            _balanceFacade = balanceFacade;
            _currentAccountProvider = currentAccountProvider;
            _accountFacade = accountFacade;
            _mapper = mapper.DefaultContext.Mapper;
        }

        /// <summary>
        /// Displays account settings
        /// </summary>
        [Authorize(Policy = "HasAccount")]
        public IActionResult Index()
        {
            var account = _currentAccountProvider.GetCurrentAccount(HttpContext.User);
            ViewData["expenses"] = GetAllPermanentExpenses(account);
            return View();
        }

        private List<IndexPermanentExpenseViewModel> GetAllPermanentExpenses(Account account)
        {
            var filter = new CostInfoFilter()
            {
                AccountId = account.Id,
                Periodicity = Periodicity.Day
            };

            var expenses = _balanceFacade.ListItem(filter);

            filter.Periodicity = Periodicity.Week;
            expenses.AddRange(_balanceFacade.ListItem(filter));

            filter.Periodicity = Periodicity.Month;
            expenses.AddRange(_balanceFacade.ListItem(filter));

            return _mapper.Map<List<IndexPermanentExpenseViewModel>>(expenses);
        }

        /// <summary>
        /// Displays view for user which doesn't have account yet
        /// </summary>
        public IActionResult NoAccount()
        {
            return View();
        }

        /// <summary>
        /// Creates currently logged-in user new account
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAccount()
        {
            var account = _currentAccountProvider.GetCurrentAccount(HttpContext.User);

            if (account != null)
            {
                TempData["ErrorMessage"] = "You already have an account, you can't create new one";
                return RedirectToAction("Index", "Error");
            }

            var user = _currentAccountProvider.GetCurrentUser(HttpContext.User);
            _accountFacade.CreateAccount(user.Id);

            TempData["SuccessMessage"] = "Account successfuly created";
            return RedirectToAction("Index", "Expense");
        }
    }
}