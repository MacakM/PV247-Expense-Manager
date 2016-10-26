using System;
using System.Data.Entity;
using DAL.Enums;
using DAL.Infrastructure;
using Microsoft.Extensions.Options;
using Riganti.Utils.Infrastructure.Core;

namespace DAL
{
    public class ExpenseManagerUnitOfWork : EntityFrameworkUnitOfWork
    {
        internal new ExpenseDbContext Context => (ExpenseDbContext)base.Context;

        public ExpenseManagerUnitOfWork(IUnitOfWorkProvider provider, DbContextOptions options) 
            : base(provider, options) { }
    }
}
