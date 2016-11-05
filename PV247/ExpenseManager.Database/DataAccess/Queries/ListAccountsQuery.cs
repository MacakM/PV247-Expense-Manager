using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    public class ListAccountsQuery : ExpenseManagerQuery<AccountModel>
    {
        public ListAccountsQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        public AccountFilter Filter { get; set; }

        protected override IQueryable<AccountModel> GetQueryable()
        {
            IQueryable<AccountModel> accounts = Context.Accounts;

            if (Filter == null)
            {
                return accounts;
            }
            if (!string.IsNullOrEmpty(Filter.Name))
            {
                accounts = Filter.DoExactMatch ? accounts.Where(account => account.Name.Equals(Filter.Name)) : accounts.Where(account => account.Name.Contains(Filter.Name));
            }
            return accounts;
        }
    }
}
