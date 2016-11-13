using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseManager.Business.DataTransferObjects.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseManager.Presentation.Authentication
{
    /// <summary>
    /// Requirement for having given access type rights
    /// </summary>
    public class HasAccessRightsRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Type of required right
        /// </summary>
        public AccountAccessType AccessType { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="accessType"></param>
        public HasAccessRightsRequirement(AccountAccessType accessType)
        {
            AccessType = accessType;
        }
    }
}
