using System.ComponentModel.DataAnnotations;
using BL.Infrastructure.DTOs;

namespace BL.DTOs
{
    public class PlanDTO : ExpenseManagerDTO<int>
    {
        /// <summary>
        /// User that created this plan.
        /// </summary>
        public int UserId { get; set; }

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
