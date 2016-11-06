using ExpenseManager.Business.Facades;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly AccountFacade _userFacade;

        public HomeController(AccountFacade userFacade)
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
