using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseManager.Presentation.Authentication
{
    /// <summary>
    /// Decides wheter user has account
    /// </summary>
    public class HasAccountHandler : AuthorizationHandler<HasAccountRequirement>
    {
        private readonly ICurrentAccountProvider _currentAccountProvider;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="currentAccountProvider"></param>
        public HasAccountHandler(ICurrentAccountProvider currentAccountProvider)
        {
            _currentAccountProvider = currentAccountProvider;
        }

        /// <inheritdoc />
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAccountRequirement requirement)
        {
            var account = _currentAccountProvider.GetCurrentAccount(context.User);

            if (account == null)
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
