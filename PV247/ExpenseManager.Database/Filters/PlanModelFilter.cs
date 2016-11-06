using System;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Database.Filters
{
    /// <summary>
    /// Filter userd in queries in order to get plans with specifies parameters
    /// </summary>
    public class PlanModelFilter : FilterModelBase
    {
        /// <summary>
        /// Account id to be used in filter
        /// </summary>
        public int? AccountId { get; set; }
        /// <summary>
        /// Account name to be used in filter
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Cost type id to be used in filter
        /// </summary>
        public int? CostTypeId { get; set; }
        /// <summary>
        /// Cost type name to be used in filter
        /// </summary>
        public string CostTypeName { get; set; }
        /// <summary>
        /// Determines if query shuold 
        /// </summary>
        public bool DoExactMatch { get; set; }
        /// <summary>
        /// Description of plan to be used in filter
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Plan type to be used in filter
        /// </summary>
        public PlanTypeModel? PlanType { get; set; }
        /// <summary>
        /// Left edge of planned money range
        /// </summary>
        public int? PlannedMoneyFrom { get; set; }
        /// <summary>
        /// Right edge of planned money range
        /// </summary>
        public int? PlannedMoneyTo { get; set; }
        /// <summary>
        /// Left edge of deadline range
        /// </summary>
        public DateTime? DeadlineFrom { get; set; }
        /// <summary>
        /// Right edge of deadline range
        /// </summary>
        public DateTime? DeadlineTo { get; set; }
        /// <summary>
        /// Filters based on completition of plan
        /// </summary>
        public bool? IsCompleted { get; set; }
    }
}
