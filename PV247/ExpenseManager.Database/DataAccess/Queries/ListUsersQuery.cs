using System;
using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;
using System.Data.Entity;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for users.
    /// </summary>
    public class ListUsersQuery : ExpenseManagerQuery<UserModel, UserModelFilter>
    {
        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public ListUsersQuery(IUnitOfWorkProvider provider) : base(provider)
        {

        }
        /// <summary>
        /// User filter.
        /// </summary>
        public override UserModelFilter Filter { get; set; }
        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
        protected override IQueryable<UserModel> GetQueryable()
        {
            IQueryable<UserModel> users = Context.Users.Include(x => x.Account);

            if (Filter == null)
            {
                return users;
            }
            if (Filter.AccountId != null)
            {
                users = users.Where(user => user.Account.Id == Filter.AccountId.Value);
            }
            if (!string.IsNullOrEmpty(Filter.AccountName))
            {
                users = Filter.DoExactMatch ? users.Where(user => user.Account.Name.Equals(Filter.AccountName)) : users.Where(user => user.Account.Name.Contains(Filter.AccountName));
            }
            if (!string.IsNullOrEmpty(Filter.Name))
            {
                users = Filter.DoExactMatch ? users.Where(user => user.Name.Equals(Filter.Name)) : users.Where(user => user.Name.Contains(Filter.Name));
            }
            if (!string.IsNullOrEmpty(Filter.Email))
            {
                users = Filter.DoExactMatch ? users.Where(user => user.Name.Equals(Filter.Email)) : users.Where(user => user.Name.Contains(Filter.Email));
            }
            if (Filter.AccessType != null)
            {
                users = users.Where(user => user.AccessType == Filter.AccessType.Value);
            }
            if (Filter.OrderByDesc == null || string.IsNullOrEmpty(Filter.OrderByPropertyName))
            {
                return users;
            }
            System.Reflection.PropertyInfo prop = typeof(UserModel).GetProperty(Filter.OrderByPropertyName);
            if (prop == null)
            {
                return users;
            }
            users = Filter.OrderByDesc.Value ? QueryOrderByHelper.OrderByDesc(users, Filter.OrderByPropertyName) : QueryOrderByHelper.OrderBy(users, Filter.OrderByPropertyName);
            if (Filter.PageNumber != null)
            {
                users = users.Skip(Math.Max(0, Filter.PageNumber.Value - 1) * Filter.PageSize);
            }
            return users.Take(Filter.PageSize);
        }
    }
}
