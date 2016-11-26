using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Presentation.Models.Plan
{
    /// <summary>
    /// Index view model
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// All plans to display
        /// </summary>
        public List<PlanViewModel> AllPlans { get; set; }

        /// <summary>
        /// Plans which is possible to close
        /// </summary>
        public List<PlanViewModel> ClosablePlans { get; set; }

        /// <summary>
        /// Currently loged-in user
        /// </summary>
        public User.IndexViewModel CurrentUser { get; set; }
    }
}
