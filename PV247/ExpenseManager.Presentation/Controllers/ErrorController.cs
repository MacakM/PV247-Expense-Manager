using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers
{
    /// <summary>
    /// Controller for handling and displaying errors
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Displays error message
        /// </summary>
        public IActionResult Index()
        {
            var message = TempData["ErrorMessage"] ?? "Unknown error occured";

            return View(message);
        }
    }
}