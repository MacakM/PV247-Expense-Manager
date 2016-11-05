using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    public class ListUsersQuery : ExpenseManagerQuery<UserModel>
    {
        public ListUsersQuery(IUnitOfWorkProvider provider) : base(provider)
        {

        }

        public UserFilter Filter { get; set; }

        protected override IQueryable<UserModel> GetQueryable()
        {
            IQueryable<UserModel> users = Context.Users;

            if (Filter == null)
            {
                return users;
            }
            if (!string.IsNullOrEmpty(Filter.Name))
            {
                users = Filter.DoExactMatch ? users.Where(account => account.Name.Equals(Filter.Name)) : users.Where(account => account.Name.Contains(Filter.Name));
            }
            if (!string.IsNullOrEmpty(Filter.Email))
            {
                users = Filter.DoExactMatch ? users.Where(user => user.Name.Equals(Filter.Email)) : users.Where(user => user.Name.Contains(Filter.Email));
            }
            if (Filter.AccessType != null)
            {
                users = users.Where(user => user.AccessType == Filter.AccessType.Value);
            }

            return users;
        }
    }
}
