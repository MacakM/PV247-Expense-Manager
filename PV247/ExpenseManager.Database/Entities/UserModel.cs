using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseManager.Database.Enums;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.Entities
{
    /// <summary>
    /// Class representing user.
    /// </summary>
    public class UserModel : IEntity<Guid>
    {
        /// <summary>
        /// Id of the user.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

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
        /// Account of the user.
        /// </summary>
        public AccountModel Account { get; set; }

        /// <summary>
        /// Access type of the user.
        /// </summary>
        [Required]
        public AccountAccessTypeModel AccessType { get; set; }
    }
}
