using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.Entities
{
    /// <summary>
    /// Class representing user.
    /// </summary>
    public class User : IEntity<int>
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
        /// Email of the user.
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// User's costs.
        /// </summary>
        public virtual List<CostInfo> Costs { get; set; }
        /// <summary>
        /// User's accesses to others' information.
        /// </summary>
        public virtual List<CostInfoUserAccess> Accesses { get; set; }
        /// <summary>
        /// All plans of the user.
        /// </summary>
        public virtual List<Plan> Plans { get; set; }
        
        /// <summary>
        /// All badges of the user.
        /// </summary>
        public virtual List<UserBadge> Badges { get; set; }
    }
}
