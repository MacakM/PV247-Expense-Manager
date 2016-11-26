using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.Entities
{
    /// <summary>
    /// Represents some badge that can users achieve.
    /// </summary>
    public class BadgeModel : IEntity<Guid>
    {
        /// <summary>
        /// Badge id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of Badge
        /// </summary>
        [Required]
        public string Name { get; set; }

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
        /// Accounts where the badge is assinged
        /// </summary>
        public virtual List<AccountBadgeModel> Accounts { get; set; }
    }
}
