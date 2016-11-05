using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters
{
    public class AccountBadgeFilter : FilterBase
    {
        public int? AccountId { get; set; }
        public string AccountName { get; set; }
        public bool DoExactMatch { get; set; }
        public int? BadgeId { get; set; }
        public string BadgeDescription { get; set; }
        public DateTime? AchievedFrom { get; set; }
        public DateTime? AchievedTo { get; set; }
    }
}
