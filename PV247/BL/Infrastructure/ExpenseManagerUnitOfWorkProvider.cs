using System;
using System.Data.Entity;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Infrastructure
{
    public class ExpenseManagerUnitOfWorkProvider : EntityFrameworkUnitOfWorkProvider
    {
        public ExpenseManagerUnitOfWorkProvider(IUnitOfWorkRegistry registry, Func<DbContext> dbContextFactory) 
            : base(registry, dbContextFactory) { }

        protected override EntityFrameworkUnitOfWork CreateUnitOfWork(Func<DbContext> dbContextFactory, DbContextOptions options)
        {
            return new ExpenseManagerUnitOfWork(this, dbContextFactory, options);
        }
    }
}
