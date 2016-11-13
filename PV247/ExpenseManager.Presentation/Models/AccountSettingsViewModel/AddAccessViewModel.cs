using System.ComponentModel.DataAnnotations;
using ExpenseManager.Business.DataTransferObjects.Enums;

namespace ExpenseManager.Presentation.Models.AccountSettingsViewModel
{
    /// <summary>
    /// View model for adding access to account
    /// </summary>
    public class AddAccessViewModel
    {
        /// <summary>
        /// Email of the user
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Access type of the user
        /// </summary>
        [Required]
        public AccountAccessType AccessType { get; set; }
    }
}