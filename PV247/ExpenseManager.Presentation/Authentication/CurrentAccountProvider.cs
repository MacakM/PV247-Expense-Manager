using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.Facades;
using ExpenseManager.Identity.Entities;
using Microsoft.AspNetCore.Http;
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
            var applicationUser = GetCurrentApplicationUser(principal);
            var userModel = _accountFacade.GetCurrentlySignedUser(applicationUser.Email, true);
            if (userModel == null)
            {
                throw new InvalidOperationException("Logged-in user doesn't exists in expensemanagerDB");
            }
            return !userModel.AccountId.HasValue ? null : _accountFacade.GetAccount(userModel.AccountId.Value);
        }

        private ApplicationUser GetCurrentApplicationUser(ClaimsPrincipal principal)
        {
            var user = _userManager.GetUserAsync(principal);
            return user.Result;
        }
    }
}
