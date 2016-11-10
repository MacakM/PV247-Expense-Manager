using System.ComponentModel.DataAnnotations;
using ExpenseManager.Business.DataTransferObjects.Enums;

namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// Business layer representation of UserModel object
    /// </summary>
    public class User : BusinessObject<int>
    {
        /// <summary>
        /// Name of the user.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Email of the user.
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// Account Id.
        /// </summary>
        public int? AccountId { get; set; }
        /// <summary>
        /// Name of users account
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Access type of the user.
        /// </summary>
        [Required]
        public AccountAccessType? AccessType { get; set; }
        /// <summary>
        /// Makes string representation of object based on its properties
        /// </summary>
        /// <returns>String representation of object</returns>
        public override string ToString()
        {
            return $"Name: {Name}, Email: {Email}, AccountId: {AccountId}, AccountName: {AccountName}, AccessType: {AccessType}";
        }

        /// <summary>
        /// Determites if two objects are the same one
        /// </summary>
        /// <param name="other">Object to be compared with</param>
        /// <returns>true if objects are same</returns>
        protected bool Equals(User other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Email, other.Email) && AccountId == other.AccountId && string.Equals(AccountName, other.AccountName) && AccessType == other.AccessType;
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
            return Equals((User) obj);
        }
        /// <summary>
        /// Compute hash of this object based on his properties
        /// </summary>
        /// <returns>This object hashcode</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Email != null ? Email.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ AccountId.GetHashCode();
                hashCode = (hashCode*397) ^ (AccountName != null ? AccountName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ AccessType.GetHashCode();
                return hashCode;
            }
        }
    }
}
