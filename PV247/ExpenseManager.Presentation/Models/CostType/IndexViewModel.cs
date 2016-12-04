using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Presentation.Models.CostType
{
    /// <summary>
    /// View model for index method
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Categories to display
        /// </summary>
        public List<CategoryViewModel> Categories { get; set; }

        /// <summary>
        /// Currently loged-in user
        /// </summary>
        public User.IndexViewModel CurrentUser { get; set; }
    }
}
