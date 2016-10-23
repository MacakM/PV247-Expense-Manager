using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
    /// <summary>
    /// Class representing paste.
    /// </summary>
    public class Paste : IEntity<int>
    {
        /// <summary>
        /// Id of the paste.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Guid that is in link to the paste.
        /// </summary>
        [Required]
        public string Guid { get; set; }
        /// <summary>
        /// Owner id.
        /// </summary>
        public int? OwnerId { get; set; }
        /// <summary>
        /// Author of this paste.
        /// </summary>
        [ForeignKey("OwnerId")]
        public User Owner { get; set; }
        /// <summary>
        /// All users that have access to this paste.
        /// </summary>
        public virtual List<UserPasteAccess> Users { get; set; }
        /// <summary>
        /// List of costs that are in this paste.
        /// </summary>
        public virtual List<CostInfoPaste> Costs { get; set; }
        /// <summary>
        /// Expiration date of this paste.
        /// </summary>
        public DateTime? Expiration { get; set; }
    }
}
