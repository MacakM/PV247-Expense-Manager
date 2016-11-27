using System;
using System.Collections.Generic;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.DataTransferObjects.Filters.Plans;
using ExpenseManager.Business.Facades;
using ExpenseManager.Database.Filters.Plans;
using ExpenseManager.Presentation.Authentication;
using ExpenseManager.Presentation.Models.Plan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers
{
    /// <summary>
    /// Controller to manage plans
    /// </summary>
    [Authorize]
    [Authorize(Policy = "HasAccount")]
    public class PlanController : BaseController
    {
        private readonly BalanceFacade _balanceFacade;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="currentAccountProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="balanceFacade"></param>
        public PlanController(ICurrentAccountProvider currentAccountProvider, Mapper mapper, BalanceFacade balanceFacade) : base(currentAccountProvider, mapper)
        {
            _balanceFacade = balanceFacade;
        }

        /// <summary>
        /// Displays all plans
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var account = CurrentAccountProvider.GetCurrentAccount(HttpContext.User);

            var model = new IndexViewModel()
            {
                AllPlans = GetAllPlans(account),
                ClosablePlans = GetClosablePlans(account),
                CurrentUser = Mapper.Map<Models.User.IndexViewModel>(CurrentAccountProvider.GetCurrentUser(HttpContext.User))
            };

            return View(model);
        }

        private List<PlanViewModel> GetClosablePlans(Account account)
        {
            var plans = _balanceFacade.ListAllCloseablePlans(account.Id);
            return Mapper.Map<List<PlanViewModel>>(plans);
        }

        private List<PlanViewModel> GetAllPlans(Account account)
        {
            var allPlansFilters = new List<Filter<Plan>>
            {
                new PlansByAccountId(account.Id)
            };

            var allPlans = _balanceFacade.ListPlans(allPlansFilters, null);
            return Mapper.Map<List<PlanViewModel>>(allPlans);
        }

        /// <summary>
        /// Displays form for creating new plan
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "HasFullRights")]
        public IActionResult Create()
        {
            var model = new CreateViewModel()
            {
                CostTypes = GetAllCostTypes()
            };
            return View(model);
        }

        /// <summary>
        /// Stores given plan
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "HasFullRights")]
        public IActionResult Store(CreateViewModel model)
        {
            var costType = _balanceFacade.GetItemType(model.PlannedTypeId);

            if (!ModelState.IsValid || costType == null)
            {
                ModelState.AddModelError(string.Empty, ExpenseManagerResource.InvalidInputData);
                model.CostTypes = GetAllCostTypes();
                return View("Create", model);
            }

            var plan = Mapper.Map<Plan>(model);

            var account = CurrentAccountProvider.GetCurrentAccount(HttpContext.User);

            plan.AccountId = account.Id;
            plan.Start = DateTime.Now;

            _balanceFacade.CreatePlan(plan);

            return RedirectToAction("Index", new { successMessage = ExpenseManagerResource.PlanCreated});
        }

        /// <summary>
        /// Deletes plan
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "HasFullRights")]
        public IActionResult Delete([FromForm] Guid id)
        {
            var plan = _balanceFacade.GetPlan(id);
            var account = CurrentAccountProvider.GetCurrentAccount(HttpContext.User);

            if (plan == null || plan.AccountId != account.Id)
            {
                return RedirectWithError(ExpenseManagerResource.PlanNotDeleted);
            }

            _balanceFacade.DeletePlan(id);

            return RedirectToAction("Index", new {sucessMessage = ExpenseManagerResource.PlanDeleted});
        }

        private List<Models.CostType.IndexViewModel> GetAllCostTypes()
        {
            var costTypes = _balanceFacade.ListItemTypes(null,null);
            var costTypeViewModels = Mapper.Map<List<Models.CostType.IndexViewModel>>(costTypes);
            return costTypeViewModels;
        }

        /// <summary>
        /// Marks given plan as finished
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Close([FromForm] Guid id)
        {
            var plan = _balanceFacade.GetPlan(id);
            var account = CurrentAccountProvider.GetCurrentAccount(HttpContext.User);

            if (plan == null || plan.AccountId != account.Id)
            {
                return RedirectWithError(ExpenseManagerResource.PlanNotClosedDoesntExist);
            }

            try
            {
                _balanceFacade.ClosePlan(plan);
            }
            catch (Exception)
            {
                return RedirectWithError(ExpenseManagerResource.PlanNotClosed);
            }
            return RedirectToAction("Index", new {successMessage = ExpenseManagerResource.PlanClosed});
        }
    }
}