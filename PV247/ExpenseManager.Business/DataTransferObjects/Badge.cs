using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// Business layer representation of BadgeModel object
    /// </summary>
    public class Badge : BusinessObject<Guid>
    {
        /// <summary>
        /// Description how achieve this badge.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Badge image uri.
        /// </summary>
        public string BadgeImgUri { get; set; }

        /// <summary>
        /// Name of Badge should be in PascalCase 
        /// with alpha characters only
        /// </summary>
        [RegularExpression("^[a-zA-Z]*$")]
        public string Name { get; set; }
        
        /// <summary>
        /// List of Accounts where badge is assigned
        /// </summary>
        public List<AccountBadge> Accounts { get; set; }
        
        /// <summary>
        /// Makes string representation of object based on its properties
        /// </summary>
        /// <returns>String representation of object</returns>
        public override string ToString()
        {
            return $"Description: {Description}, BadgeImgUri: {BadgeImgUri}";
        }
        
        /// <summary>
        /// Determites if two objects are the same one
        /// </summary>
        /// <param name="other">Object to be compared with</param>
        /// <returns>true if objects are same</returns>
        protected bool Equals(Badge other)
        {
            return string.Equals(Description, other.Description) && string.Equals(BadgeImgUri, other.BadgeImgUri) && Equals(Accounts, other.Accounts);
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
            return Equals((Badge) obj);
        }
       
        /// <summary>
        /// Compute hash of this object based on his properties
        /// </summary>
        /// <returns>This object hashcode</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (BadgeImgUri != null ? BadgeImgUri.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Accounts != null ? Accounts.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
