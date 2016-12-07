using System.Linq;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Users
{
    /// <summary>
    /// Filter query by access type
    /// </summary>
    internal class UsersByAccessType : IFilter<UserModel>
    {
        /// <summary>
        /// Specifies users access type to filter with
        /// </summary>
        public readonly AccountAccessTypeModel? AccessType;

        /// <summary>
        /// Filters query by access type
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
            return AccessType != null ? queryable.Where(user => user.AccessType == AccessType) : queryable;
        }

        public UsersByAccessType(AccountAccessTypeModel? accessType)
        {
            AccessType = accessType;
        }

        public UsersByAccessType(AccountAccessType? accessType)
        {
            if (accessType != null) AccessType = (AccountAccessTypeModel) accessType;
        }
    }
}