using System;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for CostInfo entity.
    /// </summary>
    internal class CostInfoRepository : ExpenseManagerRepository<CostInfoModel, Guid>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">unitOfWork provider</param>
        internal CostInfoRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
