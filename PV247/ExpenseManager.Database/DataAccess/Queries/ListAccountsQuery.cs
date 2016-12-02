using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for accounts.
    /// </summary>
    internal class ListAccountsQuery : ExpenseManagerQuery<AccountModel>
    {
        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">unitOfWork provider</param>
        internal ListAccountsQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }
        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
        protected override IQueryable<AccountModel> GetQueryable()
        {
            return ApplyFilters(Context.Accounts);
        }
    }
}
