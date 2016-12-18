using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.DataTransferObjects.Factories;
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
        private readonly ExpenseFacade _expenseFacade;

        private const int NumberOfExpensesPerPage = 10;

        /// <summary>
        /// Constructor for ExpenseController
        /// </summary>
        /// <param name="expenseFacade"></param>
        /// <param name="mapper"></param>
        /// <param name="currentAccountProvider"></param>
        public ExpenseController(ExpenseFacade expenseFacade, Mapper mapper, ICurrentAccountProvider currentAccountProvider) : base(currentAccountProvider, mapper)
        {
            _expenseFacade = expenseFacade;
        }

        /// <summary>
        /// Displays expenses for loged-in user
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(IndexFilterViewModel filterModel)
        {
            var account = CurrentAccountProvider.GetCurrentAccount(HttpContext.User);

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = filterModel.PageNumber ?? 1,
                PageSize = NumberOfExpensesPerPage,
                OrderByPropertyName = nameof(CostInfo.Created),
                OrderByDesc = true
            };

            filterModel.Expenses = GetFilteredExpenses(account.Id, Periodicity.None, filterModel.DateFrom, filterModel.DateTo, filterModel.MoneyFrom, filterModel.MoneyTo, filterModel.CostTypeId, null, pageInfo);
            filterModel.PageCount = (int)Math.Ceiling(_expenseFacade.GetCostInfosCount(account.Id, Periodicity.None, filterModel.DateFrom, filterModel.DateTo, filterModel.MoneyFrom, filterModel.MoneyTo, filterModel.CostTypeId, null) / (double)NumberOfExpensesPerPage);
            filterModel.CostTypes = GetAllCostTypes();
            filterModel.CurrentUser = Mapper.Map<Models.User.IndexViewModel>(CurrentAccountProvider.GetCurrentUser(HttpContext.User));
            return View(filterModel);
        }

      

        /// <summary>
        /// Displays form for creating new expense
        /// </summary>
        [Authorize(Policy = "HasFullRights")]
        public IActionResult Create()
        {
            var costTypes = GetAllCostTypes();
            if (costTypes.Count == 0)
            {
                return View("NoCostType");
            }

            var model = new CreateViewModel
            {
                CostTypes = costTypes,
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
            var costType = _expenseFacade.GetItemType(costInfoViewModel.TypeId);
            var account = CurrentAccountProvider.GetCurrentAccount(HttpContext.User);

            if (!ModelState.IsValid || costType == null || costType.AccountId != account.Id)
            {
                var model = new CreateViewModel()
                {
                    CostTypes = GetAllCostTypes(),
                };

                return View("Create", model);
            }

            var costInfo = Mapper.Map<CostInfo>(costInfoViewModel);


            costInfo.AccountId = account.Id;
            costInfo.Periodicity = Periodicity.None;

            _expenseFacade.CreateItem(costInfo);

            return RedirectToAction("Index", new { successMessage = ExpenseManagerResource.ExpenseCreated });
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
            var costInfo = _expenseFacade.GetItem(id);
            var account = CurrentAccountProvider.GetCurrentAccount(HttpContext.User);

            if (costInfo == null || costInfo.AccountId != account.Id)
            {
                return RedirectWithError(ExpenseManagerResource.ExpenseNotDeleted);
            }

            _expenseFacade.DeleteItem(id);

            return Redirect(returnRedirect);
        }

        private List<IndexViewModel> GetFilteredExpenses(Guid accountId, Periodicity periodicity, DateTime? dateFrom, DateTime? dateTo, decimal? moneyFrom, decimal? moneyTo, Guid? costTypeId, bool? isIncome, PageInfo pageInfo)
        {
            var expenses = _expenseFacade.ListItems(accountId, periodicity, dateFrom, dateTo,  moneyFrom, moneyTo, costTypeId,isIncome, pageInfo);
            return Mapper.Map<List<IndexViewModel>>(expenses);
        }

        private List<Models.CostType.CategoryViewModel> GetAllCostTypes()
        {
            var accountId = CurrentAccountProvider.GetCurrentAccount(HttpContext.User).Id;
            var costTypes = _expenseFacade.ListItemTypes(accountId);
            var costTypeViewModels = Mapper.Map<List<Models.CostType.CategoryViewModel>>(costTypes);
            return costTypeViewModels;
        }
    }
}