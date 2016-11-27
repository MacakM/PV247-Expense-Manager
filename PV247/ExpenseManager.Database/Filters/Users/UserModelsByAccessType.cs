using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Database.Filters.Users
{
    /// <summary>
    /// Filter query by access type
    /// </summary>
    public class UserModelsByAccessType : IFilter<UserModel>
    {
        /// <summary>
        /// Specifies users access type to filter with
        /// </summary>
        private readonly AccountAccessTypeModel _accessType;

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="accessType">Access type to be used in filter</param>
        public UserModelsByAccessType(AccountAccessTypeModel accessType)
        {
            _accessType = accessType;
        }
        /// <summary>
        /// Filters query by access type
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
            return queryable.Where(user => user.AccessType == _accessType);
        }
    }
}
