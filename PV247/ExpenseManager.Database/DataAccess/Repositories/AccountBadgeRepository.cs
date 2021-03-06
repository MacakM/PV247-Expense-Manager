﻿using System;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for UserBadge entity.
    /// </summary>
    internal class AccountBadgeRepository : ExpenseManagerRepository<AccountBadgeModel, Guid>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">unitOfWork provider</param>
        internal AccountBadgeRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
