using System;
using System.ComponentModel.DataAnnotations;
using ExpenseManager.Business.DataTransferObjects.Enums;


namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class Plan : BusinessObject<int>
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
        /// Description of the plan.
        /// </summary>
        [MaxLength(256)]
        public string Description { get; set; }
        /// <summary>
        /// Type of this plan.
        /// </summary>
        [Required]
        public PlanType? PlanType { get; set; }
        /// <summary>
        /// How much money is desired to achieve this plan.
        /// </summary>
        [Required]
        public int? PlannedMoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int? PlannedTypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PlannedTypeName { get; set; }
        /// <summary>
        /// Date when is the deadline of the plan.
        /// </summary>
        [Required]
        public DateTime? Deadline { get; set; }
        /// <summary>
        /// States whether this plan is achieved.
        /// </summary>
        public bool IsCompleted { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"AccountId: {AccountId}, AccountName: {AccountName}, Description: {Description}, PlanType: {PlanType}, PlannedMoney: {PlannedMoney}, PlannedTypeId: {PlannedTypeId}, PlannedTypeName: {PlannedTypeName}, Deadline: {Deadline}, IsCompleted: {IsCompleted}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(Plan other)
        {
            return AccountId == other.AccountId && string.Equals(AccountName, other.AccountName) && string.Equals(Description, other.Description) && PlanType == other.PlanType && PlannedMoney == other.PlannedMoney && PlannedTypeId == other.PlannedTypeId && string.Equals(PlannedTypeName, other.PlannedTypeName) && Deadline.Equals(other.Deadline) && IsCompleted == other.IsCompleted;
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
            return Equals((Plan) obj);
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
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ PlanType.GetHashCode();
                hashCode = (hashCode*397) ^ PlannedMoney.GetHashCode();
                hashCode = (hashCode*397) ^ PlannedTypeId.GetHashCode();
                hashCode = (hashCode*397) ^ (PlannedTypeName != null ? PlannedTypeName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Deadline.GetHashCode();
                hashCode = (hashCode*397) ^ IsCompleted.GetHashCode();
                return hashCode;
            }
        }
    }
}
