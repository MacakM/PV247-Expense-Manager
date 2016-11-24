using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseManager.Presentation.Authentication;
using ExpenseManager.Presentation.Models.Error;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers
{
    /// <summary>
    /// Controller for handling and displaying errors
    /// </summary>
    public class ErrorController :  BaseController
    {
        /// <summary>
        /// Displays error message
        /// </summary>
        public IActionResult Index([FromQuery] string errorMessage)
        {
            var model = new IndexViewModel()
            {
                Message = errorMessage ?? ExpenseManagerResource.UnknownError
            };

            return View(model);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="currentAccountProvider"></param>
        /// <param name="mapper"></param>
        public ErrorController(ICurrentAccountProvider currentAccountProvider, Mapper mapper) : base(currentAccountProvider, mapper)
        {
        }
    }
}