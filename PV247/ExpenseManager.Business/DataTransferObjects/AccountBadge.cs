using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountBadge : BusinessObject<int>
    {
        /// <summary>
        /// Account Id.
        /// </summary>
        [Required]
        public int? AccountId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Badge id.
        /// </summary>
        /// 
        [Required]
        public int? BadgeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(256)]
        public string BadgeDescription { get; set; }
        /// <summary>
        /// Badge image uri.
        /// </summary>
        [MaxLength(1024)]
        public string BadgeImgUri { get; set; }
        /// <summary>
        /// Date when the badge was achieved.
        /// </summary>
        [Required]
        public DateTime? Achieved { get; set; }
    }
}

