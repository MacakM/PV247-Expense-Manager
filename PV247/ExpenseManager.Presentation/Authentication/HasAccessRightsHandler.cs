using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseManager.Presentation.Authentication
{
    /// <summary>
    /// Decides whether user has given rights
    /// </summary>
    public class HasAccessRightsHandler : AuthorizationHandler<HasAccessRightsRequirement>
    {
        private readonly ICurrentAccountProvider _currentAccountProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="currentAccountProvider"></param>
        public HasAccessRightsHandler(ICurrentAccountProvider currentAccountProvider)
        {
            _currentAccountProvider = currentAccountProvider;
        }

        /// <inheritdoc />
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAccessRightsRequirement requirement)
        {
            var user = _currentAccountProvider.GetCurrentUser(context.User);
            if (user.AccessType != requirement.AccessType)
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}