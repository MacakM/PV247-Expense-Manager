using System;
using ExpenseManager.Business.DataTransferObjects.Enums;

namespace ExpenseManager.Presentation.Models.Plan
{
    /// <summary>
    /// View model for displaying plan info
    /// </summary>
    public class PlanViewModel : ViewModelId
    {
        /// <summary>
        /// Account Id.
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Name of plans account
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Description of the plan.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Type of this plan.
        /// </summary>
        public PlanType PlanType { get; set; }

        /// <summary>
        /// How much money is desired to achieve this plan.
        /// </summary>
        public decimal PlannedMoney { get; set; }

        /// <summary>
        /// Planned type id
        /// </summary>
        public Guid PlannedTypeId { get; set; }

        /// <summary>
        /// Plan type name.
        /// </summary>
        public string PlannedTypeName { get; set; }

        /// <summary>
        /// Date when is the deadline of the plan.
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Date when is the plan starts
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// States whether this plan is achieved.
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}