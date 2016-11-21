using System;

namespace ExpenseManager.Business.DataTransferObjects.Filters
{
    /// <summary>
    /// Filter userd in queries in order to get account badges with specifies parameters
    /// </summary>
    public class AccountBadgeFilter : FilterBase
    {
        /// <summary>
        /// Account id to be filtered with
        /// </summary>
        public Guid? AccountId { get; set; }
        /// <summary>
        /// Account name to be filtered with
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        ///  Determines if Equals() or Contains() should be used when matching string parameters
        /// </summary>
        public bool DoExactMatch { get; set; }
        /// <summary>
        /// Badge id to be filtered with
        /// </summary>
        public Guid? BadgeId { get; set; }
        /// <summary>
        /// Badge description to be filtered with
        /// </summary>
        public string BadgeDescription { get; set; }
        /// <summary>
        /// Left edge of achieved time range
        /// </summary>
        public DateTime? AchievedFrom { get; set; }
        /// <summary>
        /// Right edge of achieved time range
        /// </summary>
        public DateTime? AchievedTo { get; set; }
    }
}
