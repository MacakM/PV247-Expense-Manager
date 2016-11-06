using System.Collections.Generic;

namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// Business layer representation of AccountModel object
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
        /// Makes string representation of object based on its properties
        /// </summary>
        /// <returns>String representation of object</returns>
        public override string ToString()
        {
            return $"Name: {Name}";
        }
        /// <summary>
        /// Determites if two objects are the same one
        /// </summary>
        /// <param name="other">Object to be compared with</param>
        /// <returns>true if objects are same</returns>
        protected bool Equals(Account other)
        {
            return string.Equals(Name, other.Name);
        }
        /// <summary>
        /// Determites if two objects are the same one
        /// </summary>
        /// <param name="obj">Object to be compared with</param>
        /// <returns>true if objects are same</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Account) obj);
        }
        /// <summary>
        /// Compute hash of this object based on his properties
        /// </summary>
        /// <returns>This object hashcode</returns>
        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}
