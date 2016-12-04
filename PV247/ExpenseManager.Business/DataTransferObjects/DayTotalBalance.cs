using System;

namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// Holds total balance in given day
    /// </summary>
    public class DayTotalBalance
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
