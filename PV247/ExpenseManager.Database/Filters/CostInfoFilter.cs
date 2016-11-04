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
    public class CostInfoFilter : FilterBase
    {
        public bool? IsIncome { get; set; }
        public int? MoneyFrom { get; set; }
        public int? MoneyTo { get; set; }
        public int? AccountId { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public int TypeId { get; set; }
        public bool? IsPeriodic { get; set; }
    }
}
