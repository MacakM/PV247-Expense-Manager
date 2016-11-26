using System.Data.Entity;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.UnitOfWork;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.Infrastructure.Query
{
    /// <summary>
    /// A base implementation of query object in Entity Framework.
    /// </summary>
    public abstract class ExpenseManagerQuery<TResult> : QueryBase<TResult>
    {
        private readonly IUnitOfWorkProvider _provider;

        /// <summary>
        /// Filter used to determine parameters of query
        /// </summary>
        public FilterModelBase<TResult> Filter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseManagerQuery{TResult}"/> class.
        /// </summary>
        protected ExpenseManagerQuery(IUnitOfWorkProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Gets the <see cref="DbContext"/>.
        /// </summary>
        internal ExpenseDbContext Context => (ExpenseDbContext)ExpenseManagerUnitOfWork.TryGetDbContext(_provider);
    }
}
