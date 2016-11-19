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
using ExpenseManager.Presentation.Models.AccountSettingsViewModel;
using ExpenseManager.Presentation.Models.Expense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IndexViewModel = ExpenseManager.Presentation.Models.User.IndexViewModel;

namespace ExpenseManager.Presentation.Controllers
{
    /// <summary>
    /// Controller for displaying account settings
    /// </summary>
    [Authorize]
    public class AccountSettingsController : BaseController
    {
        private readonly BalanceFacade _balanceFacade;
        private readonly AccountFacade _accountFacade;

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
            AccountFacade accountFacade) : base(currentAccountProvider, mapper)
        {
            _balanceFacade = balanceFacade;
            _accountFacade = accountFacade;
        }

        /// <summary>
        /// Displays account settings
        /// </summary>
        [Authorize(Policy = "HasAccount")]
        public IActionResult Index()
        {
            var account = _currentAccountProvider.GetCurrentAccount(HttpContext.User);
            ViewData["expenses"] = GetAllPermanentExpenses(account);
            ViewData["usersWithAccess"] = GetAllUsersWithAccess(account);
            return View();
        }

        private List<IndexViewModel> GetAllUsersWithAccess(Account account)
        {
            var userFilter = new UserFilter()
            {
                AccountId = account.Id
            };

            var users = _accountFacade.ListUsers(userFilter);

            return _mapper.Map<List<IndexViewModel>>(users);
        }

        private List<IndexPermanentExpenseViewModel> GetAllPermanentExpenses(Account account)
        {
            var filter = new CostInfoFilter()
            {
                AccountId = account.Id,
                Periodicity = Periodicity.Day
            };

            var expenses = _balanceFacade.ListItems(filter);

            filter.Periodicity = Periodicity.Week;
            expenses.AddRange(_balanceFacade.ListItems(filter));

            filter.Periodicity = Periodicity.Month;
            expenses.AddRange(_balanceFacade.ListItems(filter));

            return _mapper.Map<List<IndexPermanentExpenseViewModel>>(expenses);
        }

        /// <summary>
        /// Adds access to new user for this account
        /// </summary>
        [Authorize(Policy = "HasAccount")]
        [Authorize(Policy = "HasFullRights")]
        public IActionResult AddAccessToAccount(AddAccessViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectWithError("Invalid input data");
            }

            var user = GetUserFromEmail(model.Email);
            if (user == null)
            {
                return RedirectWithError("User with given email doesn't exits");
            }
            if (user.AccountId != null)
            {
                return RedirectWithError("User with given email already has an account");
            }

            var account = _currentAccountProvider.GetCurrentAccount(HttpContext.User);

            _accountFacade.AttachAccountToUser(user.Id, account.Id, model.AccessType);

            TempData["SuccessMessage"] = "Access to user was successfully granted";
            return RedirectToAction("Index");

        }

        private User GetUserFromEmail(string email)
        {
            var userFilter = new UserFilter()
            {
                Email = email
            };

            var users = _accountFacade.ListUsers(userFilter);
            return users.FirstOrDefault();
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
                return RedirectWithError("You already have an account, you can't create new one");
            }

            var user = _currentAccountProvider.GetCurrentUser(HttpContext.User);
            _accountFacade.CreateAccount(user.Id);

            TempData["SuccessMessage"] = "Account successfuly created";
            return RedirectToAction("Index", "Expense");
        }
    }
}