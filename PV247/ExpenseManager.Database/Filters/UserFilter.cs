using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Database.Filters
{
    public class UserFilter : FilterBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool ExactMatch { get; set; }
        public AccountAccessType? AccessType { get; set; }
    }
}
