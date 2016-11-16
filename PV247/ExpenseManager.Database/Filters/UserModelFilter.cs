using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Database.Filters
{
    /// <summary>
    /// Filter userd in queries in order to get users with specifies parameters
    /// </summary>
    public class UserModelFilter : FilterModelBase<UserModel>
    {
        /// <summary>
        /// Specifies users name to filter with
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Specifies users email to filter with
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Specifies account id to filter with
        /// </summary>
        public int? AccountId { get; set; }

        /// <summary>
        /// Specifies account name to filter with
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Determines if Equals() or Contains() should be use while filtering with strings
        /// </summary>
        public bool DoExactMatch { get; set; }

        /// <summary>
        /// Specifies users access type to filter with
        /// </summary>
        public AccountAccessTypeModel? AccessType { get; set; }

        /// <summary>
        /// Filters given query
        /// </summary>
        /// <param name="queryable">Query to be filtered</param>
        public override IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
            if (AccountId != null)
            {
                queryable = queryable.Where(user => user.Account.Id == AccountId.Value);
            }
            if (!string.IsNullOrEmpty(AccountName))
            {
                queryable = DoExactMatch ? queryable.Where(user => user.Account.Name.Equals(AccountName)) : queryable.Where(user => user.Account.Name.Contains(AccountName));
            }
            if (!string.IsNullOrEmpty(Name))
            {
                queryable = DoExactMatch ? queryable.Where(user => user.Name.Equals(Name)) : queryable.Where(user => user.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Email))
            {
                queryable = DoExactMatch ? queryable.Where(user => user.Email.Equals(Email)) : queryable.Where(user => user.Email.Contains(Email));
            }
            if (AccessType != null)
            {
                queryable = queryable.Where(user => user.AccessType == AccessType.Value);
            }
            if (OrderByDesc == null || string.IsNullOrEmpty(OrderByPropertyName))
            {
                return queryable;
            }
            System.Reflection.PropertyInfo prop = typeof(UserModel).GetProperty(OrderByPropertyName);
            if (prop == null)
            {
                return queryable;
            }
            queryable = OrderByDesc.Value ? QueryOrderByHelper.OrderByDesc(queryable, OrderByPropertyName) : QueryOrderByHelper.OrderBy(queryable, OrderByPropertyName);
            if (PageNumber != null)
            {
                queryable = queryable.Skip(Math.Max(0, PageNumber.Value - 1) * PageSize);
            }
            return queryable.Take(PageSize);
        }
    }
}