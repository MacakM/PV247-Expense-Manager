using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseManager.Presentation.Authentication;
using ExpenseManager.Presentation.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseManager.Presentation.Controllers
{
    /// <summary>
    /// Base class for other controllers
    /// </summary>
    public class BaseController : Controller
    {

        /// <summary>
        /// current account provider
        /// </summary>
        protected ICurrentAccountProvider _currentAccountProvider;

        /// <summary>
        /// mapper
        /// </summary>
        protected readonly IRuntimeMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="currentAccountProvider"></param>
        /// <param name="mapper"></param>
        public BaseController(
            ICurrentAccountProvider currentAccountProvider, 
            Mapper mapper)
        {
            _currentAccountProvider = currentAccountProvider;
            _mapper = mapper.DefaultContext.Mapper;
        }

        /// <summary>
        /// Retirets to error page with message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected IActionResult RedirectWithError(string message)
        {
            return RedirectToAction("Index", "Error", new { errorMessage = message });
        }
    }
}