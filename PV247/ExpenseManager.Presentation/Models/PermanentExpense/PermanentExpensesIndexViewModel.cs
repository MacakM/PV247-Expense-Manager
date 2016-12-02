using System.Collections.Generic;

namespace ExpenseManager.Presentation.Models.PermanentExpense
{
    /// <summary>
    /// ViewModel for permanent expenses
    /// </summary>
    public class PermanentExpensesIndexViewModel
    {
        /// <summary>
        /// Currently logged in user
        /// </summary>
        public User.IndexViewModel CurrentUser { get; set; }

        /// <summary>
        /// Expenses of current user
        /// </summary>
        public List<IndexPermanentExpenseViewModel> Expenses { get; set; }
    }
}
