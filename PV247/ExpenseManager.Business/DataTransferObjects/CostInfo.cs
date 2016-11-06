using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// Business layer representation of CostInfoModel object
    /// </summary>
    public class CostInfo : BusinessObject<int>
    {
        /// <summary>
        /// State whether set money is income or outcome.
        /// </summary>
        public bool IsIncome { get; set; }
        /// <summary>
        /// How much money has changed.
        /// </summary>
        [Required]
        public int? Money { get; set; }
        /// <summary>
        /// Account id.
        /// </summary>
        [Required]
        public int? AccountId { get; set; }
        /// <summary>
        /// Account whom this cost belongs.
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Date when the cost info was created.
        /// </summary>
        [Required]
        public DateTime? Created { get; set; }
        /// <summary>
        /// Type id.
        /// </summary>
        [Required]
        public int? TypeId { get; set; }
        /// <summary>
        /// Type of the cost.
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// State whether this cost is periodic each month.
        /// </summary>
        public bool IsPeriodic { get; set; }
        /// <summary>
        /// Makes string representation of object based on its properties
        /// </summary>
        /// <returns>String representation of object</returns>
        public override string ToString()
        {
            return $"IsIncome: {IsIncome}, Money: {Money}, AccountId: {AccountId}, AccountName: {AccountName}, Created: {Created}, TypeId: {TypeId}, TypeName: {TypeName}, IsPeriodic: {IsPeriodic}";
        }
        /// <summary>
        /// Determites if two objects are the same one
        /// </summary>
        /// <param name="other">Object to be compared with</param>
        /// <returns>true if objects are same</returns>
        protected bool Equals(CostInfo other)
        {
            return IsIncome == other.IsIncome && Money == other.Money && AccountId == other.AccountId && string.Equals(AccountName, other.AccountName) && Created.Equals(other.Created) && TypeId == other.TypeId && string.Equals(TypeName, other.TypeName) && IsPeriodic == other.IsPeriodic;
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
            return Equals((CostInfo) obj);
        }
        /// <summary>
        /// Compute hash of this object based on his properties
        /// </summary>
        /// <returns>This object hashcode</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = IsIncome.GetHashCode();
                hashCode = (hashCode*397) ^ Money.GetHashCode();
                hashCode = (hashCode*397) ^ AccountId.GetHashCode();
                hashCode = (hashCode*397) ^ (AccountName != null ? AccountName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Created.GetHashCode();
                hashCode = (hashCode*397) ^ TypeId.GetHashCode();
                hashCode = (hashCode*397) ^ (TypeName != null ? TypeName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ IsPeriodic.GetHashCode();
                return hashCode;
            }
        }
    }
}
