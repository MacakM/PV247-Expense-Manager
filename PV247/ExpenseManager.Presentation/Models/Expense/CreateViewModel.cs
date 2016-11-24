using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Presentation.Models.Expense
{
    /// <summary>
    /// Model for creating expenses
    /// </summary>
    public class CreateViewModel
    {
        /// <summary>
        /// State whether set money is income or outcome.
        /// </summary>
        [Required]
        public bool IsIncome { get; set; }
        /// <summary>
        /// How much money has changed.
        /// </summary>
        [Required]
        public decimal Money { get; set; }
        /// <summary>
        /// More concrete description of the cost
        /// </summary>
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// Type of the cost.
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// Cost types to choose
        /// </summary>
        public List<CostType.IndexViewModel> CostTypes { get; set; }

        /// <summary>
        /// Error message to display
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
