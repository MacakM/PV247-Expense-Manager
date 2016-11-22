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

        /// <summary>
        /// Currently displayed expenses
        /// </summary>
        public List<IndexViewModel> Expenses { get; set; }

        /// <summary>
        /// Number of pages with current filtering
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Cost types to filter by
        /// </summary>
        public List<CostType.IndexViewModel> CostTypes { get; set; }
    }
}
