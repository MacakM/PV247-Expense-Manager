using System.Data.Entity;
using ExpenseManager.Database.Infrastructure.UnitOfWork;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.Infrastructure.Query
{
    /// <summary>
    /// A base implementation of query object in Entity Framework.
    /// </summary>
    public abstract class ExpenseManagerQuery<TResult> : QueryBase<TResult>
    {
        private readonly IUnitOfWorkProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseManagerQuery{TResult}"/> class.
        /// </summary>
        public ExpenseManagerQuery(IUnitOfWorkProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Gets the <see cref="DbContext"/>.
        /// </summary>
        internal ExpenseDbContext Context
        {
            get { return (ExpenseDbContext)ExpenseManagerUnitOfWork.TryGetDbContext(provider); }
        }

    }
}
