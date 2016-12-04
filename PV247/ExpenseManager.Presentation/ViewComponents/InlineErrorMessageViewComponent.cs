using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.ViewComponents
{
    /// <summary>
    /// Component for displaying inline error message
    /// </summary>
    public class InlineErrorMessageViewComponent : ViewComponent
    {
        /// <summary>
        /// View component invokation
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            string errorMessage = HttpContext.Request.Query["errorMessage"];
            return View<string>(errorMessage);
        }
    }
}
