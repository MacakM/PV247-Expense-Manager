using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.Entities
{
    /// <summary>
    /// Represents some badge that can users achieve.
    /// </summary>
    public class Badge : IEntity<int>
    {
        /// <summary>
        /// Badge id.
        /// </summary>
        public int Id { get; set; }
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
        public virtual List<UserBadge> Users { get; set; }
    }
}
