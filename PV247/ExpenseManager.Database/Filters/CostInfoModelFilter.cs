using System;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Database.Filters
{
    /// <summary>
    /// Filter userd in queries in order to get cost infos with specifies parameters
    /// </summary>
    public class CostInfoModelFilter : FilterModelBase
    {
        /// <summary>
        /// Filter of income if false, do not filter if is null
        /// </summary>
        public bool? IsIncome { get; set; }
        /// <summary>
        /// Left edge of money range
        /// </summary>
        public decimal? MoneyFrom { get; set; }
        /// <summary>
        /// Right edge of money range
        /// </summary>
        public decimal? MoneyTo { get; set; }
        /// <summary>
        /// Account id to be filtered with
        /// </summary>
        public int? AccountId { get; set; }
        /// <summary>
        /// Account name to be filtered with
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Determines if Equals() or Contains() should be used when matching string parameters
        /// </summary>
        public bool DoExactMatch { get; set; }
        /// <summary>
        /// Left edge of created range
        /// </summary>
        public DateTime? CreatedFrom { get; set; }
        /// <summary>
        /// Right edge of created range
        /// </summary>
        public DateTime? CreatedTo { get; set; }
        /// <summary>
        /// Type id to be filtered with
        /// </summary>
        public int? TypeId { get; set; }
        /// <summary>
        /// Type name to be 
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// Periodicity of cost
        /// </summary>
        public PeriodicityModel? Periodicity { get; set; }
        /// <summary>
        /// Mulptiplies periodicity
        /// </summary>
        public int? PeriodicMultiplicityFrom { get; set; }
        /// <summary>
        /// Mulptiplies periodicity
        /// </summary>
        public int? PeriodicMultiplicityTo { get; set; }
    }
}
