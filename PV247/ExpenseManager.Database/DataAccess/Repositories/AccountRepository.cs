using System;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for Account entity.
    /// </summary>
    public class AccountRepository : ExpenseManagerRepository<AccountModel, Guid>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public AccountRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
