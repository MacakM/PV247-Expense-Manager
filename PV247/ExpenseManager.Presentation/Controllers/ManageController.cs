using System.Linq;
using System.Threading.Tasks;
using ExpenseManager.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExpenseManager.Presentation.Models.ManageViewModels;

namespace ExpenseManager.Presentation.Controllers
{
    /// <summary>
    /// Controller for managing user
    /// </summary>
    [Authorize]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly ILogger _logger;

        /// <summary>
        /// Constctructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="loggerFactory"></param>
        public ManageController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<ManageController>();
        }

        /// <summary>
        /// GET: /Manage/Index
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            var statusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? ExpenseManagerResource.PasswordChanged
                : message == ManageMessageId.SetPasswordSuccess ? ExpenseManagerResource.PasswordSet
                : message == ManageMessageId.SetTwoFactorSuccess ? ExpenseManagerResource.TwoFactorAuthProviderSet
                : message == ManageMessageId.Error ? ExpenseManagerResource.UnknownError
                : message == ManageMessageId.AddPhoneSuccess ? ExpenseManagerResource.PhoneNumberAdded
                : message == ManageMessageId.RemovePhoneSuccess ? ExpenseManagerResource.PhoneNumberRemoved
                : "";

            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return RedirectToAction("Index", "Error");
            }
            var model = new IndexViewModel
            {
                HasPassword = await _userManager.HasPasswordAsync(user),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user),
                TwoFactor = await _userManager.GetTwoFactorEnabledAsync(user),
                Logins = await _userManager.GetLoginsAsync(user),
                BrowserRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user),
                StatusMessage = statusMessage
            };
            return View(model);
        }

        /// <summary>
        /// POST: /Manage/RemoveLogin
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel account)
        {
            ManageMessageId? message = ManageMessageId.Error;
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.RemoveLoginAsync(user, account.LoginProvider, account.ProviderKey);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    message = ManageMessageId.RemoveLoginSuccess;
                }
            }
            return RedirectToAction(nameof(ManageLogins), new { Message = message });
        }


        /// <summary>
        /// POST: /Manage/EnableTwoFactorAuthentication
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableTwoFactorAuthentication()
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true);
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation(1, "User enabled two-factor authentication.");
            }
            return RedirectToAction(nameof(Index), "Manage");
        }

        /// <summary>
        /// POST: /Manage/DisableTwoFactorAuthentication
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableTwoFactorAuthentication()
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, false);
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation(2, "User disabled two-factor authentication.");
            }
            return RedirectToAction(nameof(Index), "Manage");
        }


        /// <summary>
        /// GET: /Manage/ManageLogins
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ManageLogins(ManageMessageId? message = null)
        {
            var statusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? ExpenseManagerResource.ExternalLoginRemoved
                : message == ManageMessageId.AddLoginSuccess ? ExpenseManagerResource.ExternalLoginAdded
                : message == ManageMessageId.Error ? ExpenseManagerResource.UnknownError
                : "";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return RedirectToAction("Index", "Error");
            }
            var userLogins = await _userManager.GetLoginsAsync(user);
            var otherLogins = _signInManager.GetExternalAuthenticationSchemes().Where(auth => userLogins.All(ul => auth.AuthenticationScheme != ul.LoginProvider)).ToList();
            var showRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins,
                ShowRemoveButton = showRemoveButton,
                StatusMessage = statusMessage
            });
        }

        /// <summary>
        /// POST: /Manage/LinkLogin
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Action("LinkLoginCallback", "Manage");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
            return Challenge(properties, provider);
        }

        /// <summary>
        /// GET: /Manage/LinkLoginCallback
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> LinkLoginCallback()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return RedirectToAction("Index", "Error");
            }
            var info = await _signInManager.GetExternalLoginInfoAsync(await _userManager.GetUserIdAsync(user));
            if (info == null)
            {
                return RedirectToAction(nameof(ManageLogins), new { Message = ManageMessageId.Error });
            }
            var result = await _userManager.AddLoginAsync(user, info);
            var message = result.Succeeded ? ManageMessageId.AddLoginSuccess : ManageMessageId.Error;
            return RedirectToAction(nameof(ManageLogins), new { Message = message });
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        /// <summary>
        /// Messages
        /// </summary>
        public enum ManageMessageId
        {
            /// <summary>
            /// phone added successfully
            /// </summary>
            AddPhoneSuccess,
            /// <summary>
            /// login added successfully
            /// </summary>
            AddLoginSuccess,
            /// <summary>
            /// password changed successfully
            /// </summary>
            ChangePasswordSuccess,
            /// <summary>
            /// two factor auth set successfully
            /// </summary>
            SetTwoFactorSuccess,
            /// <summary>
            /// password set successfully
            /// </summary>
            SetPasswordSuccess,
            /// <summary>
            /// login removed successfully
            /// </summary>
            RemoveLoginSuccess,
            /// <summary>
            /// phone removed successfully
            /// </summary>
            RemovePhoneSuccess,
            /// <summary>
            /// unknown error
            /// </summary>
            Error
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

    }
}
