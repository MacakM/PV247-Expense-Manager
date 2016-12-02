using System;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using ExpenseManager.Database.Infrastructure.ConnectionConfiguration;
using Microsoft.Extensions.Options;
using Riganti.Utils.Infrastructure.Core;

[assembly: InternalsVisibleTo("ExpenseManager.Business.Tests")]
namespace ExpenseManager.Database.Infrastructure.UnitOfWork
{
    /// <summary>
    /// An implementation of unit of work provider in Entity Framework.
    /// </summary>
    public class ExpenseManagerUnitOfWorkProvider : UnitOfWorkProviderBase
    {
        internal Func<DbContext> DbContextFactory { get; private set; }

        internal ConnectionOptions ConnectionOptions { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionOptions">connection options</param>
        /// <param name="registry">Storage for unitOfWork instances</param>
        public ExpenseManagerUnitOfWorkProvider(ConnectionOptions connectionOptions,
            IUnitOfWorkRegistry registry)
            : base(registry)
        {
            ConnectionOptions = connectionOptions;
        }

        /// <summary>
        /// Alternative variant with db context factory for injecting custom Db Context (currently used for testing)
        /// </summary>
        /// <param name="dbContextFactory">db context factory</param>
        /// <param name="registry">Storage for unitOfWork instances</param>
        internal ExpenseManagerUnitOfWorkProvider(Func<DbContext> dbContextFactory, IUnitOfWorkRegistry registry)            
            : base(registry)
        {
            DbContextFactory = dbContextFactory;
        }

        /// <summary>
        /// Creates the unit of work with specified options.
        /// </summary>
        public IUnitOfWork Create(bool reuseParentContext = true)
        {
            return CreateCore(reuseParentContext);
        }

        /// <summary>
        /// Creates the unit of work.
        /// </summary>
        protected sealed override IUnitOfWork CreateUnitOfWork(object parameter)
        {
            return parameter is bool ? CreateUnitOfWork((bool) parameter) : CreateUnitOfWork(true);
        }

        /// <summary>
        /// Creates the unit of work.
        /// </summary>
        protected virtual ExpenseManagerUnitOfWork CreateUnitOfWork(bool reuseParentContext)
        {
            return new ExpenseManagerUnitOfWork(this, reuseParentContext);
        }
    }
}