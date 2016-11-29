using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Business.DataTransferObjects.Filters.Users
{
    /// <summary>
    /// Filter query by access type
    /// </summary>
    public class UsersByAccessType : IFilter<UserModel>
    {
        /// <summary>
        /// Specifies users access type to filter with
        /// </summary>
        public AccountAccessTypeModel AccessType { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="accessType">Access type to be used in filter</param>
        public UsersByAccessType(AccountAccessTypeModel accessType)
        {
            AccessType = accessType;
        }

        /// <summary>
        /// Filters query by access type
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<UserModel> FilterQuery(IQueryable<UserModel> queryable)
        {
            return queryable.Where(user => user.AccessType == AccessType);
        }
    }
}