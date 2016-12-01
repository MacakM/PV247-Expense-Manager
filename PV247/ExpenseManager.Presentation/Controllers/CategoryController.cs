using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.Facades;
using ExpenseManager.Presentation.Authentication;
using ExpenseManager.Presentation.Models.CostType;
using ExpenseManager.Presentation.ViewComponents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers
{
    /// <summary>
    /// Controller for managing categories
    /// </summary>
    [Authorize]
    [Authorize(Policy = "HasAccount")]
    public class CategoryController : BaseController
    {
        private readonly BalanceFacade _balanceFacade;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="currentAccountProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="balanceFacade"></param>
        public CategoryController(ICurrentAccountProvider currentAccountProvider, Mapper mapper, BalanceFacade balanceFacade) : base(currentAccountProvider, mapper)
        {
            _balanceFacade = balanceFacade;
        }

        /// <summary>
        /// Displays all cost types for current account
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var account = CurrentAccountProvider.GetCurrentAccount(HttpContext.User);
            var model = new IndexViewModel()
            {
                Categories = Mapper.Map<List<CategoryViewModel>>(_balanceFacade.ListItemTypes(account.Id)),
                CurrentUser = GetCurrentUser()
            };
            return View(model);
        }

        /// <summary>
        /// For creating category
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "HasFullRights")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Stores category
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "HasFullRights")]
        public IActionResult Store(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Error", new {errorMessage = ExpenseManagerResource.InvalidInputData});
            }

            var existingCategories = _balanceFacade.ListItemTypes(model.Name, null);

            if (existingCategories.Count != 0)
            {
                return RedirectToAction("Index", "Error", new {errorMessage = ExpenseManagerResource.CategoryExists});
            }

            var account = CurrentAccountProvider.GetCurrentAccount(HttpContext.User);
            var costType = Mapper.Map<CostType>(model);
            costType.AccountId = account.Id;

            _balanceFacade.CreateItemType(costType);

            return RedirectToAction("Index", new {successMessage = ExpenseManagerResource.CategoryCreated});
        }

        private Models.User.IndexViewModel GetCurrentUser()
        {
            return Mapper.Map<Models.User.IndexViewModel>(CurrentAccountProvider.GetCurrentUser(HttpContext.User));
        }
    }
}