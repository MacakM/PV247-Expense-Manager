using AutoMapper;
using ExpenseManager.Presentation.Authentication;
using ExpenseManager.Presentation.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.ViewComponents
{
    /// <summary>
    /// Component for providing user email
    /// </summary>
    public class UserEmailViewComponent : ViewComponent
    {
        private readonly ICurrentAccountProvider _currentAccountProvider;

        private readonly IRuntimeMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="currentAccountProvider"></param>
        /// <param name="mapper"></param>
        public UserEmailViewComponent(ICurrentAccountProvider currentAccountProvider, Mapper mapper)
        {
            _currentAccountProvider = currentAccountProvider;
            _mapper = mapper.DefaultContext.Mapper;
        }

        /// <summary>
        /// Resolves current view component
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            var user = _currentAccountProvider.GetCurrentUser(HttpContext.User);
            var model = _mapper.Map<IndexViewModel>(user);
            return View("~/Views/Partial/_UserEmail.cshtml", model);
        }
    }
}
