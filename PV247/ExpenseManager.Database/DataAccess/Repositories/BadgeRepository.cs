using System;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for Badge entity.
    /// </summary>
    public class BadgeRepository : ExpenseManagerRepository<BadgeModel, Guid>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public BadgeRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
