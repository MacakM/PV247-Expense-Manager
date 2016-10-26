using DAL.Infrastructure.ConnectionConfiguration;
using Microsoft.Extensions.Options;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Infrastructure
{
    /// <summary>
    /// An implementation of unit of work provider in Entity Framework.
    /// </summary>
    public class ExpenseManagerUnitOfWorkProvider : UnitOfWorkProviderBase
    {
        internal IOptions<ConnectionOptions> ConnectionOptions { get; }

        public ExpenseManagerUnitOfWorkProvider(IOptions<ConnectionOptions> connectionOptions,
            IUnitOfWorkRegistry registry)
            : base(registry)
        {
            ConnectionOptions = connectionOptions;
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