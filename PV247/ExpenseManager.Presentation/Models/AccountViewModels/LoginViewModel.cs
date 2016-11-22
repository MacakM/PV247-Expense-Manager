using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Presentation.Models.AccountViewModels
{
    /// <summary>
    /// View model for loggin-in
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Email to login by
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password to login by
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Whether to remember user
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// URL to redirect to after user is authenticated
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
