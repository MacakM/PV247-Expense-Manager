using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Range(0.0001, Double.MaxValue)]
        public decimal Money { get; set; }

        /// <summary>
        /// More concrete description of the cost
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Date when the cost info was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Type of the cost.
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// Cost types to choose
        /// </summary>
        public List<CostType.CategoryViewModel> CostTypes { get; set; }

        /// <summary>
        /// Errors which occured while creating
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }
}
