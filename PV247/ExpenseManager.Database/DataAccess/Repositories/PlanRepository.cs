using System;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for Plan entity.
    /// </summary>
    internal class PlanRepository : ExpenseManagerRepository<PlanModel, Guid>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">unitOfWork provider</param>
        internal PlanRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
