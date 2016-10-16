using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    /// <summary>
    /// Represents information about user's costs.
    /// </summary>
    public class CostInfo
    {
        /// <summary>
        /// Id of the cost info.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// State whether set money is income or outcome.
        /// </summary>
        public bool IsIncome { get; set; }
        /// <summary>
        /// How much money has changed.
        /// </summary>
        public int Money { get; set; }
        /// <summary>
        /// User whom this cost belongs.
        /// </summary>
        [Required]
        public User User { get; set; }
        /// <summary>
        /// Type of the cost.
        /// </summary>
        [Required]
        public CostType Type { get; set; }
        /// <summary>
        /// State whether this cost is periodic each month.
        /// </summary>
        public bool IsPeriodic { get; set; }
        /// <summary>
        /// List of pastes that contains is this cost info.
        /// </summary>
        public virtual List<Paste> Pastes { get; set; }
    }
}
