using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for accounts.
    /// </summary>
    public class ListAccountsQuery : ExpenseManagerQuery<AccountModel>
    {
        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public ListAccountsQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }
        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
        protected override IQueryable<AccountModel> GetQueryable()
        {
            IQueryable<AccountModel> accounts = Context.Accounts;

            return Filter == null ? accounts : Filter.FilterQuery(accounts);
        }
    }
}
