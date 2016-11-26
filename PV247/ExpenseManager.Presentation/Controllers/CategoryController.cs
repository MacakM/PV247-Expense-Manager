using AutoMapper;
using ExpenseManager.Presentation.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers
{
    [Authorize]
    [Authorize(Policy = "HasAccount")]
    public class CategoryController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public CategoryController(ICurrentAccountProvider currentAccountProvider, Mapper mapper) : base(currentAccountProvider, mapper)
        {
        }
    }
}