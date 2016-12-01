﻿using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Users
{
    /// <summary>
    /// Filter used to filter users by their email
    /// </summary>
    internal class UsersByEmail : IFilter<UserModel>
    {
        /// <summary>
        /// Specifies users email to filter with
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Filters users by their email
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
            return queryable.Where(user => user.Email.Contains(Email));
        }
    }
}