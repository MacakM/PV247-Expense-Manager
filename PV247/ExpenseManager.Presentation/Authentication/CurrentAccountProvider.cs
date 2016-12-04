using System;
using System.Security.Claims;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.Facades;
using ExpenseManager.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace ExpenseManager.Presentation.Authentication
{
    /// <summary>
    /// Class which is responsible for getting account of user
    /// who is currenty logged-in
    /// </summary>
    public class CurrentAccountProvider : ICurrentAccountProvider
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly AccountFacade _accountFacade;

        /// <summary>
        /// Constructor
        /// </summary>
        public CurrentAccountProvider(UserManager<ApplicationUser> userManager, AccountFacade accountFacade)
        {
            _userManager = userManager;
            _accountFacade = accountFacade;
        }

        /// <summary>
        /// Gets current account
        /// </summary>
        public Account GetCurrentAccount(ClaimsPrincipal principal)
        {
            var user = GetCurrentUser(principal);
            if (user == null)
            {
                return null;
            }

            return _accountFacade.GetAccount(user.AccountId);
        }

        /// <inheritdoc />
        public User GetCurrentUser(ClaimsPrincipal principal)
        {
            // Simpler solution:
            return _accountFacade.GetCurrentlySignedUser(principal.Identity.Name, true);

            /* temporarily commented out
            var applicationUser = GetCurrentApplicationUser(principal);
            return _accountFacade.GetCurrentlySignedUser(applicationUser.Email, true);
            */
        }

        private ApplicationUser GetCurrentApplicationUser(ClaimsPrincipal principal)
        {
            var user = _userManager.GetUserAsync(principal);
            return user.Result;
        }
    }
}
