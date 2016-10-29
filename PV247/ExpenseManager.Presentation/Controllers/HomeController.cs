using ExpenseManager.Bussines.Facades;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers
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
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
