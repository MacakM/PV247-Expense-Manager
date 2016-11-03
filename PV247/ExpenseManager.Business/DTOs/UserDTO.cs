
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Business.DTOs
{
    public class UserDTO : ExpenseManagerDTO<int>
    {
        /// <summary>
        /// Name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Account of the user.
        /// </summary>
        public AccountDTO AccountDTO { get; set; }

        /// <summary>
        /// Access type of the user.
        /// </summary>
        public AccountAccessType AccessType { get; set; }

        protected bool Equals(UserDTO other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Email, other.Email) &&
                Equals(AccountDTO, other.AccountDTO) && AccessType == other.AccessType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.GetType() == GetType() && Equals((UserDTO) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ (Email?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (AccountDTO?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (int) AccessType;
                return hashCode;
            }
        }
    }
}
