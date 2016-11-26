using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.Entities
{
    /// <summary>
    /// Represents table of achieved badges by users.
    /// </summary>
    public class AccountBadgeModel : IEntity<Guid>
    {
        /// <summary>
        /// Id of the achievement of user.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Account Id.
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Account.
        /// </summary>
        [Required]
        [ForeignKey("AccountId")]
        public virtual AccountModel Account { get; set; }

        /// <summary>
        /// Badge id.
        /// </summary>
        public Guid BadgeId { get; set; }

        /// <summary>
        /// Badge that is achieved by user.
        /// </summary>
        [Required]
        [ForeignKey("BadgeId")]
        public virtual BadgeModel Badge { get; set; }

        /// <summary>
        /// Date when the badge was achieved.
        /// </summary>
        [DataType(DataType.Date)]
        [Required]
        public DateTime? Achieved { get; set; } = DateTime.Now;
    }
}
