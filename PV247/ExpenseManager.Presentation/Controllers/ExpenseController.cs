using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Facades;
using ExpenseManager.Presentation.Models.Expense;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers
{

    /// <summary>
    /// Controller for managing expenses
    /// </summary>
    public class ExpenseController : Controller
    {
        private readonly BalanceFacade _balanceFacade;
        private readonly IRuntimeMapper _mapper;

        /// <summary>
        /// Constructor for ExpenseController
        /// </summary>
        /// <param name="balanceFacade"></param>
        /// <param name="mapper"></param>
        public ExpenseController(BalanceFacade balanceFacade, Mapper mapper)
        {
            _balanceFacade = balanceFacade;
            _mapper = mapper.DefaultContext.Mapper;
        }

        /// <summary>
        /// Displays expenses for loged-in user
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var filter = new CostInfoFilter()
            {
                AccountName = "testerAccount"
            };

            var expenses = _balanceFacade.ListItem(filter);
            var indexViewModels = _mapper.Map<List<IndexViewModel>>(expenses);
            return View(indexViewModels);
        }
    }
}