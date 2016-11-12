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
    public class ExpenseController : Controller
    {
        private readonly BalanceFacade _balanceFacade;
        private readonly ICurrentAccountProvider _currentAccountProvider;
        private readonly IRuntimeMapper _mapper;

        /// <summary>
        /// Constructor for ExpenseController
        /// </summary>
        /// <param name="balanceFacade"></param>
        /// <param name="mapper"></param>
        /// <param name="currentAccountProvider"></param>
        public ExpenseController(BalanceFacade balanceFacade, Mapper mapper, ICurrentAccountProvider currentAccountProvider)
        {
            _balanceFacade = balanceFacade;
            _currentAccountProvider = currentAccountProvider;
            _mapper = mapper.DefaultContext.Mapper;
        }

        /// <summary>
        /// Displays expenses for loged-in user
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(IndexFilterViewModel filterModel)
        {
            var account = _currentAccountProvider.GetCurrentAccount(HttpContext.User);
            // todo users without account should not be allowed here
            // todo pagination
            var filter = new CostInfoFilter()
            {
                AccountId = account.Id,
                Periodicity = Periodicity.None,
                CreatedFrom = filterModel.DateFrom,
                CreatedTo = filterModel.DateTo,
                MoneyFrom = filterModel.MoneyFrom,
                MoneyTo = filterModel.MoneyTo,
                TypeId = filterModel.CostTypeId
            };
            
            ViewData["indexViewModels"] = GetFilteredExpenses(filter);
            ViewData["costTypes"] = GetAllCostTypes();
            return View(filterModel);
        }


        /// <summary>
        /// Displays form for creating new expense
        /// </summary>
        public IActionResult Create()
        {
            ViewData["costTypes"] = GetAllCostTypes();
            return View();
        }


        /// <summary>
        /// Stores new expense
        /// </summary>
        [HttpPost]
        public IActionResult Store(CreateViewModel costInfoViewModel)
        {
            var costType = _balanceFacade.GetItemType(costInfoViewModel.TypeId);

            if (!ModelState.IsValid || costType == null)
            {
                TempData["CreateExpenseMessage"] = "Invalid input data";
                return RedirectToAction("Create");
            }

            var costInfo = _mapper.Map<CostInfo>(costInfoViewModel);

            var account = _currentAccountProvider.GetCurrentAccount(HttpContext.User);

            costInfo.AccountId = account.Id;
            costInfo.Created = DateTime.Now;
            costInfo.Periodicity = Periodicity.None;

            _balanceFacade.CreateItem(costInfo);

            TempData["SuccessMessage"] = "Expense successfully created";

            return RedirectToAction("Index");
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