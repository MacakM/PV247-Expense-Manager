using AutoMapper;
using ExpenseManager.Presentation.Authentication;
using Microsoft.AspNetCore.Mvc;

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
        protected ICurrentAccountProvider CurrentAccountProvider;

        /// <summary>
        /// mapper
        /// </summary>
        protected readonly IRuntimeMapper Mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="currentAccountProvider"></param>
        /// <param name="mapper"></param>
        public BaseController(
            ICurrentAccountProvider currentAccountProvider, 
            Mapper mapper)
        {
            CurrentAccountProvider = currentAccountProvider;
            Mapper = mapper.DefaultContext.Mapper;
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