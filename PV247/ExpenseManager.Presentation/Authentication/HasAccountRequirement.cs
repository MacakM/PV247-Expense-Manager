using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseManager.Presentation.Authentication
{
    /// <summary>
    /// Requiremenet for having account
    /// </summary>
    public class HasAccountRequirement : IAuthorizationRequirement
    {
    }
}
