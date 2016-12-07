using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Users
{
    /// <summary>
    /// Filters by user name
    /// </summary>
    internal class UserModelsByName : IFilter<UserModel>
    {
        /// <summary>
        /// User name
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Filters by user name
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
            return Name != null ? queryable.Where(user => user.Name.Contains(Name)) : queryable;
        }

        public UserModelsByName(string name)
        {
            Name = name;
        }
    }
}