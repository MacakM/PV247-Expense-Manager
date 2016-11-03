using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Business.DTOs
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
