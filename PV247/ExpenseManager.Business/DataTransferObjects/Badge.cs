using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Business.DataTransferObjects
{
    public class Badge : ExpenseManager<int>
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
