using DAL.Enums;
using Microsoft.Extensions.Options;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Infrastructure
{
    /// <summary>
    /// An implementation of unit of work provider in Entity Framework.
    /// </summary>
    public class EntityFrameworkUnitOfWorkProvider : UnitOfWorkProviderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkUnitOfWorkProvider"/> class.
        /// </summary>
        protected EntityFrameworkUnitOfWorkProvider(IUnitOfWorkRegistry registry) : base(registry) { }

        /// <summary>
        /// Creates the unit of work with specified options.
        /// </summary>
        public IUnitOfWork Create(DbContextOptions options)
        {
            return CreateCore(options);
        }

        /// <summary>
        /// Creates the unit of work.
        /// </summary>
        protected sealed override IUnitOfWork CreateUnitOfWork(object parameter)
        {
            var options = (parameter as DbContextOptions?) ?? DbContextOptions.ReuseParentContext;
            return CreateUnitOfWork(options);
        }

        /// <summary>
        /// Creates the unit of work.
        /// </summary>
        protected virtual EntityFrameworkUnitOfWork CreateUnitOfWork(DbContextOptions options)
        {
            return new EntityFrameworkUnitOfWork(this, options);
        }
    }
}