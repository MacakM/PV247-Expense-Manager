
using ExpenseManager.Database.Enums;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.DataTransferObjects
{
    public class User : ExpenseManager<int>
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
        public Account Account { get; set; }

        /// <summary>
        /// Access type of the user.
        /// </summary>
        public AccountAccessType AccessType { get; set; }

        protected bool Equals(User other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Email, other.Email) &&
                Equals(Account, other.Account) && AccessType == other.AccessType;
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
            return obj.GetType() == GetType() && Equals((User) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ (Email?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (Account?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (int) AccessType;
                return hashCode;
            }
        }
    }
}
