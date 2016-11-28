using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseManager.Business.Facades;
using ExpenseManager.Presentation.Authentication;
using ExpenseManager.Presentation.Models.ViewComponent;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.ViewComponents
{
    /// <summary>
    /// Component for displaying graph in right menu
    /// </summary>
    public class GraphViewComponent : ViewComponent
    {
        private readonly ICurrentAccountProvider _currentAccountProvider;
        private readonly BalanceFacade _balanceFacade;
        private readonly IRuntimeMapper _mapper;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="currentAccountProvider"></param>
        /// <param name="balanceFacade"></param>
        /// <param name="mapper"></param>
        public GraphViewComponent(ICurrentAccountProvider currentAccountProvider,
                                  BalanceFacade balanceFacade,
                                  Mapper mapper)
        {
            _currentAccountProvider = currentAccountProvider;
            _balanceFacade = balanceFacade;
            _mapper = mapper.DefaultContext.Mapper;
        }

        /// <summary>
        /// Invokes view component
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            var account = _currentAccountProvider.GetCurrentAccount(HttpContext.User);
            var balances = _balanceFacade.GetDailyBalanceGraphData(account.Id);
            var model = _mapper.Map<List<DayTotalBalanceViewModel>>(balances);
            return View("~/Views/Partial/_RightMenuGraph.cshtml", model);
        }
    }
}
