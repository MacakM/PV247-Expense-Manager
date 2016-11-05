using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountBadge : BusinessObject<int>
    {
        /// <summary>
        /// Account Id.
        /// </summary>
        [Required]
        public int? AccountId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Badge id.
        /// </summary>
        /// 
        [Required]
        public int? BadgeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(256)]
        public string BadgeDescription { get; set; }
        /// <summary>
        /// Badge image uri.
        /// </summary>
        [MaxLength(1024)]
        public string BadgeImgUri { get; set; }
        /// <summary>
        /// Date when the badge was achieved.
        /// </summary>
        [Required]
        public DateTime? Achieved { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"AccountName: {AccountName}, BadgeDescription: {BadgeDescription}, Achieved: {Achieved}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(AccountBadge other)
        {
            return AccountId == other.AccountId && string.Equals(AccountName, other.AccountName) && BadgeId == other.BadgeId && string.Equals(BadgeDescription, other.BadgeDescription);
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
            return Equals((AccountBadge) obj);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

