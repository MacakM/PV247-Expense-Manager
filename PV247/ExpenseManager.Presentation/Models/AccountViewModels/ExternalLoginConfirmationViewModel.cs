using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Presentation.Models.AccountViewModels
{
    /// <summary>
    /// View model for extrnal login confirmation
    /// </summary>
    public class ExternalLoginConfirmationViewModel
    {
        /// <summary>
        /// Used email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// URL to redirect to after user is authenticated
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Login provider
        /// </summary>
        public string LoginProvider { get; set; }
    }
}
