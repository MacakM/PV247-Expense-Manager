using System.Security.Claims;
using ExpenseManager.Business.DataTransferObjects;

namespace ExpenseManager.Presentation.Authentication
{
    /// <summary>
    /// Class which is responsible for getting account of user
    /// who is currenty logged-in
    /// </summary>
    public interface ICurrentAccountProvider
    {
        /// <summary>
        /// Gets current account
        /// </summary>
        Account GetCurrentAccount(ClaimsPrincipal principal);

        /// <summary>
        /// Gets current user
        /// </summary>
        User GetCurrentUser(ClaimsPrincipal principal);
    }
}
