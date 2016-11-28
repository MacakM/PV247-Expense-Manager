using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Database.Filters.Users
{
    /// <summary>
    /// Filter query by access type
    /// </summary>
    public class UserModelsByAccessType : FilterModel<UserModel>
    {
        /// <summary>
        /// Specifies users access type to filter with
        /// </summary>
        public AccountAccessTypeModel AccessType { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="accessType">Access type to be used in filter</param>
        public UserModelsByAccessType(AccountAccessTypeModel accessType)
        {
            AccessType = accessType;
        }
        /// <summary>
        /// Filters query by access type
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public override IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
            return queryable.Where(user => user.AccessType == AccessType);
        }
    }
}
