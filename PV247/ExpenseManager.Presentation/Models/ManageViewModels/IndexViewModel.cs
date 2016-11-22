using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ExpenseManager.Presentation.Models.ManageViewModels
{
    /// <summary>
    /// Index view model
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Whether user has password
        /// </summary>
        public bool HasPassword { get; set; }

        /// <summary>
        /// Logins
        /// </summary>
        public IList<UserLoginInfo> Logins { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Whether two factor authentication is used
        /// </summary>
        public bool TwoFactor { get; set; }

        /// <summary>
        /// Whether is remember
        /// </summary>
        public bool BrowserRemembered { get; set; }

        /// <summary>
        /// Status message
        /// </summary>
        public string StatusMessage { get; set; }
    }
}
