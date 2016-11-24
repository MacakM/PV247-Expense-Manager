using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Facades;
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
            var account = _currentAccountProvider.GetCurrentAccount(HttpContext.User);

            var model = new IndexViewModel()
            {
                AllPlans = GetAllPlans(account),
                ClosablePlans = GetClosablePlans(account)
            };

            return View(model);
        }

        private List<PlanViewModel> GetClosablePlans(Account account)
        {
            var plans = _balanceFacade.ListAllCloseablePlans(account.Id);
            return _mapper.Map<List<PlanViewModel>>(plans);
        }

        private List<PlanViewModel> GetAllPlans(Account account)
        {
            var allPlansFilter = new PlanFilter()
            {
                AccountId = account.Id
            };

            var allPlans = _balanceFacade.ListPlans(allPlansFilter);
            return _mapper.Map<List<PlanViewModel>>(allPlans);
        }
    }
}