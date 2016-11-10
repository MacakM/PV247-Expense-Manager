using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
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
        public async Task<IActionResult> Index()
        {
            //var account = _currentAccountProvider.GetCurrentAccount(HttpContext.User);
            var filter = new CostInfoFilter()
            {
                AccountName = "testerAccount", // todo use real account
            };


            var expenses = _balanceFacade.ListItem(filter);
            var indexViewModels = _mapper.Map<List<IndexViewModel>>(expenses);
            return View(indexViewModels);
        }

        /// <summary>
        /// Displays form for creating new expense
        /// </summary>
        public IActionResult Create()
        {
            var costTypeFilter = new CostTypeFilter(); // todo apply filter by account
            var costTypes = _balanceFacade.ListItemTypes(costTypeFilter);
            var costTypeViewModels = _mapper.Map <List<Models.CostType.IndexViewModel>>(costTypes);
            ViewData["costTypes"] = costTypeViewModels;
            return View();
        }

        /// <summary>
        /// Stores new expense
        /// </summary>
        [HttpPost]
        public IActionResult Store(CreateViewModel costInfoViewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid input data"); // todo handle errors more gracefully
            }

            var costType = _balanceFacade.GetItemType(costInfoViewModel.TypeId);

            if (costType == null)
            {
                throw new InvalidOperationException("Invalid input data"); // todo handle errors more gracefully
            }

            var costInfo = _mapper.Map<CostInfo>(costInfoViewModel);

            // todo add actual account here

            _balanceFacade.CreateItem(costInfo);

            return RedirectToAction("Index");
        }
    }
}