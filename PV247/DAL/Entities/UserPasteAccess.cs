using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    /// <summary>
    /// Represents table that stores which user can access which paste.
    /// </summary>
    public class UserPasteAccess
    {
        /// <summary>
        /// Id of the access.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// User.
        /// </summary>
        [Required]
        public User User { get; set; }
        /// <summary>
        /// Paste that is accessible to user.
        /// </summary>
        [Required]
        public Paste Paste { get; set; }
    }
}
