using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Database.Filters;

namespace ExpenseManager.Business.DataTransferObjects.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class UserFilter : FilterModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? AccountId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool DoExactMatch { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AccountAccessType? AccessType { get; set; }
    }
}
