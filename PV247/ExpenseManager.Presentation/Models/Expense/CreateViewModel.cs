using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [Range(typeof(DateTime), "1/1/1900", "1/1/3000")]
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
        /// Cost types for select box
        /// </summary>
        public IEnumerable<SelectListItem> SelectCostTypes
        {
            get
            {
                return CostTypes?.Select(
                           costType => new SelectListItem() {Value = costType.Id.ToString(), Text = costType.Name}) ?? new List<SelectListItem>();
            }
        }
    }
}
