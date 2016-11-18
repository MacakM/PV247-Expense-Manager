using System;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for CostType entity.
    /// </summary>
    public class CostTypeRepository : ExpenseManagerRepository<CostTypeModel, Guid>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public CostTypeRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
