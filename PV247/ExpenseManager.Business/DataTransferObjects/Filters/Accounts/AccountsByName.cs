﻿using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Accounts
{
    /// <summary>
    /// Filter userd in queries in order to get accounts with specifies parameters
    /// </summary>
    internal class AccountsByName : IFilter<AccountModel>
    {
        /// <summary>
        /// Name that has to match in filtered accounts
        /// </summary>
        public readonly string Name;
        
        /// <summary>
        /// Filters given query
        /// </summary>
        /// <param name="queryable">Query to be filtered</param>
        public IQueryable<AccountModel> FilterQuery(IQueryable<AccountModel> queryable)
        {
            return Name!= null ? queryable.Where(account => account.Name.Contains(Name)) : queryable;
        }

        public AccountsByName(string name)
        {
            Name = name;
        }
    }
}
