using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Presentation.Models.Expense
{
    /// <summary>
    /// ViewModel to filter costs
    /// </summary>
    public class IndexFilterViewModel
    {
        /// <summary>
        /// Date from which to search costs
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Date to which to search costs
        /// </summary>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Minimal ammount for costs
        /// </summary>
        public decimal? MoneyFrom { get; set; }

        /// <summary>
        /// Maximal ammount for costs
        /// </summary>
        public decimal? MoneyTo { get; set; }

        /// <summary>
        /// Cost type id to filter by
        /// </summary>
        public Guid? CostTypeId { get; set; }

        /// <summary>
        /// Number of page to display
        /// </summary>
        public int? PageNumber { get; set; }
    }
}
