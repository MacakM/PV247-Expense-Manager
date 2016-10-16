using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    /// <summary>
    /// Class representing paste.
    /// </summary>
    public class Paste
    {
        /// <summary>
        /// Id of the paste.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Owner id.
        /// </summary>
        public int OwnerId { get; set; }
        /// <summary>
        /// Author of this paste.
        /// </summary>
        [Required]
        [ForeignKey("OwnerId")]
        public User Owner { get; set; }
        /// <summary>
        /// All users that have access to this paste.
        /// </summary>
        public virtual List<UserPasteAccess> Users { get; set; }
        /// <summary>
        /// List of costs that are in this paste.
        /// </summary>
        public virtual List<CostInfo> Costs { get; set; }
        /// <summary>
        /// Expiration date of this paste.
        /// </summary>
        public DateTime? Expiration { get; set; }
    }
}
