using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Presentation.Models.ViewComponent
{

    /// <summary>
    /// Holds total balance in given day
    /// </summary>
    public class DayTotalBalanceViewModel
    {
        /// <summary>
        /// Date 
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Total balance that was in given day
        /// </summary>
        public decimal TotalBalance { get; set; }
    }
}
