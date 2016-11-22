using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Presentation.Models.AccountViewModels
{
    /// <summary>
    /// ViewModel for registering new user
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Email of newly created user
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Password of newly created user
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Repeated password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Whether to create also new account with user
        /// </summary>
        [Required]
        public bool CreateAccount { get; set; }

        /// <summary>
        /// URL to redirect to after user is authenticated
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
