using System;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for Badge entity.
    /// </summary>
    internal class BadgeRepository : ExpenseManagerRepository<BadgeModel, Guid>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">unitOfWork provider</param>
        internal BadgeRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
