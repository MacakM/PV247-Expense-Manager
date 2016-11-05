using System;

namespace ExpenseManager.Database.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class CostInfoFilter : FilterBase
    {
        /// <summary>
        /// 
        /// </summary>
        public bool? IsIncome { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? MoneyFrom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? MoneyTo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? AccountId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool DoExactMatch { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreatedFrom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreatedTo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? TypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? IsPeriodic { get; set; }
    }
}
