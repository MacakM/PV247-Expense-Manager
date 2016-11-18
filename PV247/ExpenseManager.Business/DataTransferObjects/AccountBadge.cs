using System;

namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// Business layer representation of AccountBadgeModel object
    /// </summary>
    public class AccountBadge : BusinessObject<Guid>
    {
        /// <summary>
        /// Account Id.
        /// </summary>
        public Guid AccountId { get; set; }
        /// <summary>
        /// Name of account
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Badge id.
        /// </summary>
        public Guid BadgeId { get; set; }
        /// <summary>
        /// Description of badge
        /// </summary>
        public string BadgeDescription { get; set; }
        /// <summary>
        /// Badge image uri.
        /// </summary>
        public string BadgeImgUri { get; set; }
        /// <summary>
        /// Date when the badge was achieved.
        /// </summary>
        public DateTime? Achieved { get; set; }
        /// <summary>
        /// Makes string representation of object based on its properties
        /// </summary>
        /// <returns>String representation of object</returns>
        public override string ToString()
        {
            return $"AccountName: {AccountName}, BadgeDescription: {BadgeDescription}, Achieved: {Achieved}";
        }
        /// <summary>
        /// Determites if two objects are the same one
        /// </summary>
        /// <param name="other">Object to be compared with</param>
        /// <returns>true if objects are same</returns>
        protected bool Equals(AccountBadge other)
        {
            return AccountId == other.AccountId && string.Equals(AccountName, other.AccountName) && BadgeId == other.BadgeId && string.Equals(BadgeDescription, other.BadgeDescription);
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
            return Equals((AccountBadge) obj);
        }
        /// <summary>
        /// Compute hash of this object based on his properties
        /// </summary>
        /// <returns>This object hashcode</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = AccountId.GetHashCode();
                hashCode = (hashCode*397) ^ (AccountName != null ? AccountName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ BadgeId.GetHashCode();
                hashCode = (hashCode*397) ^ (BadgeDescription != null ? BadgeDescription.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}

