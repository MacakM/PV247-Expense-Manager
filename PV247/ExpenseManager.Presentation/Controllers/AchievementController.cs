using AutoMapper;
using ExpenseManager.Presentation.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers
{
    /// <summary>
    /// Achievement controller
    /// </summary>
    [Authorize]
    [Authorize(Policy = "HasAccount")]
    public class AchievementController : BaseController
    {
        /// <summary>
        /// Index page
        /// </summary>
        /// <returns></returns>
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