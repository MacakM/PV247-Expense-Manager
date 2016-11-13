using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseManager.Database.Enums;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.Entities
{
    /// <summary>
    /// Represents plan.
    /// </summary>
    public class PlanModel : IEntity<int>
    {
        /// <summary>
        /// Id of the plan.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id of the account that created this plan.
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// Account that created this plan.
        /// </summary>
        [Required]
        [ForeignKey("AccountId")]
        public AccountModel Account { get; set; }
        /// <summary>
        /// Description of the plan.
        /// </summary>
        [MaxLength(256)]
        public string Description { get; set; }
        /// <summary>
        /// Type of this plan.
        /// </summary>
        public PlanTypeModel PlanType { get; set; }
        /// <summary>
        /// How much money is desired to achieve this plan.
        /// </summary>
        public decimal PlannedMoney { get; set; }
        /// <summary>
        /// Which type of cost is assigned to this plan.
        /// </summary>
        public CostTypeModel PlannedType { get; set; }
        /// <summary>
        /// Date when is the deadline of the plan.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }
        /// <summary>
        /// States whether this plan is achieved.
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}
