using ExpenseManager.Presentation.Models.ViewComponent;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.ViewComponents
{
    /// <summary>
    /// View component for displaying success success
    /// </summary>
    public class SuccessMessageViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes componet
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            string successMessage = HttpContext.Request.Query["successMessage"];
            return View<string>(successMessage);
        }
    }
}
