using System;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Database.Filters
{
    public class PlanFilter : FilterBase
    {
      
        public int? AccountId { get; set; }
        public string Description { get; set; }
        public PlanType? PlanType { get; set; }
        public int? PlannedMoney { get; set; }
        public DateTime? DeadlineFrom { get; set; }
        public DateTime? DeadlineTo { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
