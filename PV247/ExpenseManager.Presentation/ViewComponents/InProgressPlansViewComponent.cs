using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Facades;
using ExpenseManager.Presentation.Authentication;
using ExpenseManager.Presentation.Models.Plan;
using ExpenseManager.Presentation.Models.ViewComponent;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.ViewComponents
{
    /// <summary>
    /// ViewComponent for displaying plans in progress
    /// </summary>
    public class InProgressPlansViewComponent : ViewComponent
    {

        private readonly BalanceFacade _balanceFacade;
        private readonly ICurrentAccountProvider _currentAccountProvider;
        private readonly IRuntimeMapper _mapper;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="balanceFacade"></param>
        /// <param name="currentAccountProvider"></param>
        /// <param name="mapper"></param>
        public InProgressPlansViewComponent(BalanceFacade balanceFacade, 
            ICurrentAccountProvider currentAccountProvider,
            Mapper mapper)
        {
            _balanceFacade = balanceFacade;
            _currentAccountProvider = currentAccountProvider;
            _mapper = mapper.DefaultContext.Mapper;
        }

        /// <summary>
        /// Invokes componet
        /// </summary>
        public IViewComponentResult Invoke([FromQuery] string successMessage = null)
        {
            var account = _currentAccountProvider.GetCurrentAccount(HttpContext.User);
            var model = new InProgressPlansViewModel()
            {
                Plans = _mapper.Map<List<PlanViewModel>>(_balanceFacade.ListPlansInProgress(account.Id))
            };
            return View("~/Views/Partial/_InProgressPlans.cshtml", model);
        }
    }
}
