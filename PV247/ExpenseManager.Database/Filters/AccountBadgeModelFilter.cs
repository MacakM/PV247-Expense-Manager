using System;

namespace ExpenseManager.Database.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountBadgeModelFilter : FilterModelBase
    {
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
        public int? BadgeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BadgeDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AchievedFrom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AchievedTo { get; set; }
    }
}
