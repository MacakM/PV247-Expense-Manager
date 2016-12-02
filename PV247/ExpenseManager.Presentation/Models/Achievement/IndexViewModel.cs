using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Presentation.Models.Achievement
{
    /// <summary>
    /// Index view model for achievemnts
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Badges that were already achieved
        /// </summary>
        public List<AccountBadgeViewModel> AchievedBadges { get; set; }

        /// <summary>
        /// Badges that have not been achieved yet
        /// </summary>
        public List<BadgeViewModel> NotAchievedBadges { get; set; }
    }
}
