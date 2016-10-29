using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.Entities
{
    /// <summary>
    /// Represents table of achieved badges by users.
    /// </summary>
    public class UserBadge : IEntity<int>
    {
        /// <summary>
        /// Id of the achievement of user.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// User Id.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// User.
        /// </summary>
        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; }
        /// <summary>
        /// Badge id.
        /// </summary>
        public int BadgeId { get; set; }
        /// <summary>
        /// Badge that is achieved by user.
        /// </summary>
        [Required]
        [ForeignKey("BadgeId")]
        public Badge Badge { get; set; }
    }
}
