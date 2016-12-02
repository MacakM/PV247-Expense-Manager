using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Presentation.Models.Achievement
{
    /// <summary>
    /// PL Badge representation
    /// </summary>
    public class BadgeViewModel
    {
        /// <summary>
        /// Description how achieve this badge.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Badge image uri.
        /// </summary>
        public string BadgeImgUri { get; set; }
    }
}
