using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Presentation.Models.AccountViewModels
{
    /// <summary>
    /// view for verifying cod
    /// </summary>
    public class VerifyCodeViewModel
    {
        /// <summary>
        /// provider
        /// </summary>
        [Required]
        public string Provider { get; set; }

        /// <summary>
        /// code
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// url to return
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// whether to remember browser
        /// </summary>
        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        /// <summary>
        /// wether to remember user
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}