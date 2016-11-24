using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Presentation.Models.Expense;

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

        /// <summary>
        /// Expenses of current user
        /// </summary>
        public List<IndexPermanentExpenseViewModel> Expenses { get; set; }

        /// <summary>
        /// Users with access to account
        /// </summary>
        public List<User.IndexViewModel> UsersWithAccess { get; set; }

        /// <summary>
        /// Currently logged in user
        /// </summary>
        public User.IndexViewModel CurrentUser { get; set; }
    }
}