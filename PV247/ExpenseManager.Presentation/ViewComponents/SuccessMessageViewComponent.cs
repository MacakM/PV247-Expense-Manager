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
        /// <param name="successMessage"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke([FromQuery] string successMessage = null)
        {
            successMessage = HttpContext.Request.Query["successMessage"]; // since getting it as method parameter doesn't seem to work
            var model = new SuccessMessageViewModel()
            {
                Message = successMessage
            };

            return View("~/Views/Partial/_SuccessMessage.cshtml", model);
        }
    }
}
