using BL.Facades;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserFacade _userFacade;

        public HomeController(UserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        public IActionResult Index( )
        {
            var usr = _userFacade.GetCurrentlySignedUser("demo@demo.com");
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
