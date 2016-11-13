using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseManager.Presentation.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers
{
    [Authorize]
    [Authorize(Policy = "HasAccount")]
    public class AchievementController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public AchievementController(ICurrentAccountProvider currentAccountProvider, Mapper mapper) : base(currentAccountProvider, mapper)
        {
        }
    }
}