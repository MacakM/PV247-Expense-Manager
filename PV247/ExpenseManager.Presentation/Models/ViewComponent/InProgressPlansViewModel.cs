using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseManager.Presentation.Models.Plan;

namespace ExpenseManager.Presentation.Models.ViewComponent
{
    /// <summary>
    /// View model for displaying plans in progress
    /// </summary>
    public class InProgressPlansViewModel
    {
        /// <summary>
        /// Plans to display
        /// </summary>
        public List<PlanViewModel> Plans { get; set; }
    }
}
