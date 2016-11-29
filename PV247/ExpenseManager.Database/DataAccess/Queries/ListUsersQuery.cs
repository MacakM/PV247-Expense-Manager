using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;
using System.Data.Entity;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for users.
    /// </summary>
    public class ListUsersQuery : ExpenseManagerQuery<UserModel>
    {
        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">unitOfWork provider</param>
        public ListUsersQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
        protected override IQueryable<UserModel> GetQueryable()
        {
            return ApplyFilters(Context.Users.Include(x => x.Account));
        }
    }
}
