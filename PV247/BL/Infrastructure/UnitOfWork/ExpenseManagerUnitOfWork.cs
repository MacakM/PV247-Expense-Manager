using System;
using System.Data.Entity;
using DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Infrastructure.UnitOfWork
{
    public class ExpenseManagerUnitOfWork : EntityFrameworkUnitOfWork
    {
        public new ExpenseDbContext Context => (ExpenseDbContext)base.Context;

        public ExpenseManagerUnitOfWork(IUnitOfWorkProvider provider, Func<DbContext> dbContextFactory, DbContextOptions options) 
            : base(provider, dbContextFactory, options) { }
    }
}
