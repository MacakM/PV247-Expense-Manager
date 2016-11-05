using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class CostInfo : BusinessObject<int>
    {
        /// <summary>
        /// State whether set money is income or outcome.
        /// </summary>
        public bool IsIncome { get; set; }
        /// <summary>
        /// How much money has changed.
        /// </summary>
        [Required]
        public int? Money { get; set; }
        /// <summary>
        /// Account id.
        /// </summary>
        [Required]
        public int? AccountId { get; set; }
        /// <summary>
        /// Account whom this cost belongs.
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Date when the cost info was created.
        /// </summary>
        [Required]
        public DateTime? Created { get; set; }
        /// <summary>
        /// Type id.
        /// </summary>
        [Required]
        public int? TypeId { get; set; }
        /// <summary>
        /// Type of the cost.
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// State whether this cost is periodic each month.
        /// </summary>
        public bool IsPeriodic { get; set; }
    }
}
