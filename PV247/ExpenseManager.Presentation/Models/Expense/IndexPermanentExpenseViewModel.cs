using System;
using System.ComponentModel.DataAnnotations;
using ExpenseManager.Business.DataTransferObjects.Enums;

namespace ExpenseManager.Presentation.Models.Expense
{
    /// <summary>
    /// Presentation layer representation of permanent CostInfoModel object
    /// </summary>
    public class IndexPermanentExpenseViewModel : ViewModelId
    {
        /// <summary>
        /// How much money has changed.
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// More concrete description of the cost
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Type of the cost.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Periodicity of cost
        /// </summary>
        public Periodicity Periodicity { get; set; }

        /// <summary>
        /// Mulptiplies periodicity
        /// </summary>
        public int PeriodicMultiplicity { get; set; }

        /// <summary>
        /// Date when the cost will be applied next time
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// State whether set money is income or outcome.
        /// </summary>
        [Required]
        public bool IsIncome { get; set; }
    }
}
