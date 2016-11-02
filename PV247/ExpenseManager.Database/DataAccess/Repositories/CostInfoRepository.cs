﻿using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for CostInfo entity.
    /// </summary>
    public class CostInfoRepository : ExpenseManagerRepository<CostInfo, int>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public CostInfoRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
