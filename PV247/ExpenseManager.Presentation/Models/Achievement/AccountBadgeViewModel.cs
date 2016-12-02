using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Presentation.Models.Achievement
{
    /// <summary>
    /// PL AccountBadge representation
    /// </summary>
    public class AccountBadgeViewModel
    {
        /// <summary>
        /// Description of badge
        /// </summary>
        public string BadgeDescription { get; set; }

        /// <summary>
        /// Badge image uri.
        /// </summary>
        public string BadgeImgUri { get; set; }

        /// <summary>
        /// Date when the badge was achieved.
        /// </summary>
        public DateTime Achieved { get; set; }
    }
}
