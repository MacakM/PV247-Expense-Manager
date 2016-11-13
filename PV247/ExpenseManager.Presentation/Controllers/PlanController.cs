using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers
{
    [Authorize]
    [Authorize(Policy = "HasAccount")]
    public class PlanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}