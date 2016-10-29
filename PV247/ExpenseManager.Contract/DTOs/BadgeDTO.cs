using Microsoft.Build.Framework;

namespace ExpenseManager.Contract.DTOs
{
    public class BadgeDTO : ExpenseManagerDTO<int>
    {
        /// <summary>
        /// Description how achieve this badge.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Badge image uri.
        /// </summary>
        [Required]
        public string BadgeImgUri { get; set; }
    }
}
