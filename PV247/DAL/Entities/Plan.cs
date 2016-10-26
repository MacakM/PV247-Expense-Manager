using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APILayer.Enums;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
    /// <summary>
    /// Represents plan.
    /// </summary>
    public class Plan : IEntity<int>
    {
        /// <summary>
        /// Id of the plan.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id of the user that created this plan.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// User that created this plan.
        /// </summary>
        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; }
        /// <summary>
        /// Description of the plan.
        /// </summary>
        [MaxLength(256)]
        public string Description { get; set; }
        /// <summary>
        /// Type of this plan.
        /// </summary>
        public PlanType PlanType { get; set; }
        /// <summary>
        /// How much money is desired to achieve this plan.
        /// </summary>
        public int PlannedMoney { get; set; }
        /// <summary>
        /// Which type of cost is assigned to this plan.
        /// </summary>
        public CostType PlannedType { get; set; }
        /// <summary>
        /// States whether this plan is achieved.
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}
