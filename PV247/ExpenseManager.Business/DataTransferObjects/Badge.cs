using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class Badge : BusinessObject<int>
    {
        /// <summary>
        /// Description how achieve this badge.
        /// </summary>
        [MaxLength(256)]
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// Badge image uri.
        /// </summary>
        [MaxLength(1024)]
        [Required]
        public string BadgeImgUri { get; set; }
        /// <summary>
        /// Users that achieved this Badge.
        /// </summary>
        public List<User> Users { get; set; }
    }
}
