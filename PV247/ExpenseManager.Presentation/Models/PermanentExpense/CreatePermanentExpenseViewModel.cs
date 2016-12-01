using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ExpenseManager.Business.DataTransferObjects.Enums;

namespace ExpenseManager.Presentation.Models.PermanentExpense
{
    /// <summary>
    /// Model for creating expenses
    /// </summary>
    public class CreatePermanentExpenseViewModel
    {
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
        [Required]
        public Guid TypeId { get; set; }

        /// <summary>
        /// Periodicty of expense
        /// </summary>
        [Required]
        public Periodicity Periodicity { get; set; }

        /// <summary>
        /// Mulptiplies periodicity
        /// </summary>
        [Required]
        [Range(0, int.MaxValue)]
        public int PeriodicMultiplicity { get; set; }

        /// <summary>
        /// Date when the cost will be first applied
        /// </summary>
        [Required]
        public DateTime Created { get; set; }

        /// <summary>
        /// State whether set money is income or outcome.
        /// </summary>
        [Required]
        public bool IsIncome { get; set; }

        /// <summary>
        /// Cost types to choose
        /// </summary>
        public List<CostType.IndexViewModel> CostTypes { get; set; }
    }
}
