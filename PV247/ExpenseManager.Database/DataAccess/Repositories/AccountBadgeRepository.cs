using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for UserBadge entity.
    /// </summary>
    public class AccountBadgeRepository : ExpenseManagerRepository<AccountBadge, int>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public AccountBadgeRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
