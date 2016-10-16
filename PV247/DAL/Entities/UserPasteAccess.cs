using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
    /// <summary>
    /// Represents table that stores which user can access which paste.
    /// </summary>
    public class UserPasteAccess : IEntity<int>
    {
        /// <summary>
        /// Id of the access.
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
        /// Paste id.
        /// </summary>
        public int PasteId { get; set; }
        /// <summary>
        /// Paste that is accessible to user.
        /// </summary>
        [Required]
        [ForeignKey("PasteId")]
        public Paste Paste { get; set; }
    }
}
