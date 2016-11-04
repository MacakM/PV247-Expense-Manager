using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters
{
    public class AccountFilter : FilterBase
    {
        public string Name { get; set; }
        public bool DoExactMatch { get; set; }
    }
}
