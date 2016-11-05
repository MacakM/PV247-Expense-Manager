using System.Collections.Generic;

namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class Account : BusinessObject<int>
    {
        /// <summary>
        /// Name of the account.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// List of users that have access to this account.
        /// </summary>
        public List<User> Users { get; set; }
        /// <summary>
        /// User's costs.
        /// </summary>
        public List<CostInfo> Costs { get; set; }
        /// <summary>
        /// All plans of the user.
        /// </summary>
        public List<Plan> Plans { get; set; }
        /// <summary>
        /// All badges of the user.
        /// </summary>
        public List<AccountBadge> Badges { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Name: {Name}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(Account other)
        {
            return string.Equals(Name, other.Name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Account) obj);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}
