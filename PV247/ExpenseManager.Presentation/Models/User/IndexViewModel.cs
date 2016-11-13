using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseManager.Business.DataTransferObjects.Enums;

namespace ExpenseManager.Presentation.Models.User
{
    /// <summary>
    /// Presentation layer representation of UserModel object
    /// </summary>
    public class IndexViewModel : ViewModelId
    {
        /// <summary>
        /// Name of the user.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email of the user.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Access type of the user.
        /// </summary>
        public AccountAccessType? AccessType { get; set; }

        /// <summary>
        /// Wethere user has full access rights
        /// </summary>
        public bool HasFullRights => AccessType == AccountAccessType.Full;
    }
}
