using System;
using System.ComponentModel.DataAnnotations;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class Plan : BusinessObject<int>
    {
        /// <summary>
        /// Account Id.
        /// </summary>
        [Required]
        public int? AccountId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Description of the plan.
        /// </summary>
        [MaxLength(256)]
        public string Description { get; set; }
        /// <summary>
        /// Type of this plan.
        /// </summary>
        [Required]
        public PlanType? PlanType { get; set; }
        /// <summary>
        /// How much money is desired to achieve this plan.
        /// </summary>
        [Required]
        public int? PlannedMoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int? PlannedTypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PlannedTypeName { get; set; }
        /// <summary>
        /// Date when is the deadline of the plan.
        /// </summary>
        [Required]
        public DateTime? Deadline { get; set; }
        /// <summary>
        /// States whether this plan is achieved.
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}
