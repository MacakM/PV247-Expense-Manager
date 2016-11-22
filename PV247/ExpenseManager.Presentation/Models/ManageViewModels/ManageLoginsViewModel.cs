using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;

namespace ExpenseManager.Presentation.Models.ManageViewModels
{
    /// <summary>
    /// ViewModel for managin logins
    /// </summary>
    public class ManageLoginsViewModel
    {
        /// <summary>
        /// Current logins
        /// </summary>
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        /// <summary>
        /// Other logins
        /// </summary>
        public IList<AuthenticationDescription> OtherLogins { get; set; }

        /// <summary>
        /// Status message
        /// </summary>
        public string StatusMessage { get; set; }

        /// <summary>
        /// Whether to display button
        /// </summary>
        public bool ShowRemoveButton { get; set; }
    }
}
