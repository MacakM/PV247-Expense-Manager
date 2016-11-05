using System;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Database.Filters
{
    public class PlanFilter : FilterBase
    {
        public int? AccountId { get; set; }
        public string AccountName { get; set; }
        public int? CostTypeId { get; set; }
        public string CostTypeName { get; set; }
        public bool DoExactMatch { get; set; }
        public string Description { get; set; }
        public PlanType? PlanType { get; set; }
        public int? PlannedMoneyFrom { get; set; }
        public int? PlannedMoneyTo { get; set; }
        public DateTime? DeadlineFrom { get; set; }
        public DateTime? DeadlineTo { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
