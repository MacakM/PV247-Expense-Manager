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
    /// Controller for managing expenses
    /// </summary>
    [Authorize]
    [Authorize(Policy = "HasAccount")]
    public class ExpenseController : BaseController
    {
        private readonly BalanceFacade _balanceFacade;

        private const int NumberOfExpensesPerPage = 10;

        /// <summary>
        /// Constructor for ExpenseController
        /// </summary>
        /// <param name="balanceFacade"></param>
        /// <param name="mapper"></param>
        /// <param name="currentAccountProvider"></param>
        public ExpenseController(BalanceFacade balanceFacade, Mapper mapper, ICurrentAccountProvider currentAccountProvider) : base(currentAccountProvider, mapper)
        {
            _balanceFacade = balanceFacade;
        }

        /// <summary>
        /// Displays expenses for loged-in user
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(IndexFilterViewModel filterModel)
        {
            var account = _currentAccountProvider.GetCurrentAccount(HttpContext.User);
            var filter = new CostInfoFilter()
            {
                AccountId = account.Id,
                Periodicity = Periodicity.None,
                CreatedFrom = filterModel.DateFrom,
                CreatedTo = filterModel.DateTo,
                MoneyFrom = filterModel.MoneyFrom,
                MoneyTo = filterModel.MoneyTo,
                TypeId = filterModel.CostTypeId,
                PageNumber = filterModel.PageNumber ?? 1,
                PageSize = NumberOfExpensesPerPage
            };
            
            filterModel.Expenses = GetFilteredExpenses(filter);
            filterModel.PageCount = (int)Math.Ceiling(_balanceFacade.GetCostInfosCount(filter) / (double)NumberOfExpensesPerPage);
            filterModel.CostTypes = GetAllCostTypes();
            filterModel.CurrentUser = _mapper.Map<Models.User.IndexViewModel>(_currentAccountProvider.GetCurrentUser(HttpContext.User));
            return View(filterModel);
        }


        /// <summary>
        /// Displays form for creating new expense
        /// </summary>
        [Authorize(Policy = "HasFullRights")]
        public IActionResult Create([FromQuery] string errorMessage = null)
        {
            var model = new CreateViewModel
            {
                CostTypes = GetAllCostTypes(),
                ErrorMessage = errorMessage
            };
            return View(model);
        }


        /// <summary>
        /// Stores new expense
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "HasFullRights")]
        public IActionResult Store(CreateViewModel costInfoViewModel)
        {
            var costType = _balanceFacade.GetItemType(costInfoViewModel.TypeId);

            if (!ModelState.IsValid || costType == null)
            {
                return RedirectToAction("Create", new { errorMessage = ExpenseManagerResource.InvalidInputData });
            }

            var costInfo = _mapper.Map<CostInfo>(costInfoViewModel);

            var account = _currentAccountProvider.GetCurrentAccount(HttpContext.User);

            costInfo.AccountId = account.Id;
            costInfo.Created = DateTime.Now;
            costInfo.Periodicity = Periodicity.None;

            _balanceFacade.CreateItem(costInfo);

            return RedirectToAction("Index", new { successMessage = ExpenseManagerResource.ExpenseCreated });
        }

        /// <summary>
        /// Displays form for creating permanent expenses
        /// </summary>
        [Authorize(Policy = "HasFullRights")]
        public IActionResult CreatePermanentExpense([FromQuery] string errorMessage = null)
        {
            var model = new CreatePermanentExpenseViewModel
            {
                CostTypes = GetAllCostTypes(),
                ErrorMessage = errorMessage
            };
            return View(model);
        }

        /// <summary>
        /// Stores permanent expense
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "HasFullRights")]
        public IActionResult StorePermanentExpense(CreatePermanentExpenseViewModel costInfoViewModel)
        {
            var costType = _balanceFacade.GetItemType(costInfoViewModel.TypeId);

            if (!ModelState.IsValid || costType == null)
            {
                return RedirectToAction("CreatePermanentExpense", new { errorMessage = ExpenseManagerResource.InvalidInputData });
            }

            var costInfo = _mapper.Map<CostInfo>(costInfoViewModel);

            var account = _currentAccountProvider.GetCurrentAccount(HttpContext.User);

            costInfo.AccountId = account.Id;

            _balanceFacade.CreateItem(costInfo);

            return RedirectToAction("Index", "AccountSettings", new { successMessage = ExpenseManagerResource.ExpenseCreated });
        }

        /// <summary>
        /// Deletes expense with given id
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "HasFullRights")]
        public IActionResult Delete(
            [FromForm] Guid id,
            [FromForm] string returnRedirect)
        {
            var costInfo = _balanceFacade.GetItem(id);
            var account = _currentAccountProvider.GetCurrentAccount(HttpContext.User);

            if (costInfo == null || costInfo.AccountId != account.Id)
            {
                return RedirectWithError(ExpenseManagerResource.ExpenseNotDeleted);
            }

            _balanceFacade.DeleteItem(id);

            return Redirect(returnRedirect);
        }

        #region Helpers

        private List<IndexViewModel> GetFilteredExpenses(CostInfoFilter filter)
        {
            var expenses = _balanceFacade.ListItem(filter);
            return _mapper.Map<List<IndexViewModel>>(expenses);
        }

        private List<Models.CostType.IndexViewModel> GetAllCostTypes()
        {
            var costTypes = _balanceFacade.ListItemTypes(null);
            var costTypeViewModels = _mapper.Map<List<Models.CostType.IndexViewModel>>(costTypes);
            return costTypeViewModels;
        }

        #endregion
    }
}