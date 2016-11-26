using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseManager.Presentation.Models.AccountViewModels
{
    /// <summary>
    /// View model for sending code
    /// </summary>
    public class SendCodeViewModel
    {
        /// <summary>
        /// selected provier
        /// </summary>
        public string SelectedProvider { get; set; }

        /// <summary>
        /// colection of providers
        /// </summary>
        public ICollection<SelectListItem> Providers { get; set; }

        /// <summary>
        /// url where to return
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// whether to remember
        /// </summary>
        public bool RememberMe { get; set; }
    }
}