using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    /// <summary>
    /// Represents plan.
    /// </summary>
    public class Plan
    {
        /// <summary>
        /// Id of the plan.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// User that created this plan.
        /// </summary>
        [Required]
        public User User { get; set; }
        /// <summary>
        /// Description of the plan.
        /// </summary>
        [MaxLength(256)]
        public string Description { get; set; }
        /// <summary>
        /// States whether this plan is achieved.
        /// </summary>
        public bool IsAchieved { get; set; }
    }
}
