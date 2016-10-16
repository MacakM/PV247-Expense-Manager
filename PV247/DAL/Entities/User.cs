using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    /// <summary>
    /// Class representing user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id of the user.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the user.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// User's costs.
        /// </summary>
        public virtual List<CostType> Costs { get; set; }

        /// <summary>
        /// All pastes of the user.
        /// </summary>
        public virtual List<Paste> OwnPastes { get; set; }

        /// <summary>
        /// All pastes of other users which user can see.
        /// </summary>
        public virtual List<UserPasteAccess> VisiblePastes { get; set; }
        /// <summary>
        /// All plans of the user.
        /// </summary>
        public virtual List<Plan> Plans { get; set; }
    }
}
