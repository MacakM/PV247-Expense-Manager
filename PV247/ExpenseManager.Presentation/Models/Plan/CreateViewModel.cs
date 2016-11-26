using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ExpenseManager.Business.DataTransferObjects.Enums;

namespace ExpenseManager.Presentation.Models.Plan
{
    /// <summary>
    /// ViewModel for creating new plan
    /// </summary>
    public class CreateViewModel
    {
        /// <summary>
        /// Description of the plan.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Type of this plan.
        /// </summary>
        [Required]
        public PlanType PlanType { get; set; }

        /// <summary>
        /// How much money is desired to achieve this plan.
        /// </summary>
        [Required]
        [Range(0, Double.MaxValue)]
        public decimal PlannedMoney { get; set; }

        /// <summary>
        /// Planned type id
        /// </summary>
        [Required]
        public Guid PlannedTypeId { get; set; }

        /// <summary>
        /// Date when is the deadline of the plan.
        /// </summary>
        [Required]
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Cost types to choose from
        /// </summary>
        public List<CostType.IndexViewModel> CostTypes { get; set; }
    }
}