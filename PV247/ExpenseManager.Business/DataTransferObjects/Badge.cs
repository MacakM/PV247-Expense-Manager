using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class Badge : BusinessObject<int>
    {
        /// <summary>
        /// Description how achieve this badge.
        /// </summary>
        [MaxLength(256)]
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// Badge image uri.
        /// </summary>
        [MaxLength(1024)]
        [Required]
        public string BadgeImgUri { get; set; }
        /// <summary>
        /// Users that achieved this Badge.
        /// </summary>
        public List<User> Users { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Description: {Description}, BadgeImgUri: {BadgeImgUri}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(Badge other)
        {
            return string.Equals(Description, other.Description) && string.Equals(BadgeImgUri, other.BadgeImgUri) && Equals(Users, other.Users);
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
            return Equals((Badge) obj);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (BadgeImgUri != null ? BadgeImgUri.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Users != null ? Users.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
