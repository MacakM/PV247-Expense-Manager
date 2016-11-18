using System;
using ExpenseManager.Business.DataTransferObjects.Enums;


namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// Business layer representation of PlanModel object
    /// </summary>
    public class Plan : BusinessObject<Guid>
    {
        /// <summary>
        /// Account Id.
        /// </summary>
        public Guid? AccountId { get; set; }
        /// <summary>
        /// Name of plans account
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Description of the plan.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Type of this plan.
        /// </summary>
        public PlanType PlanType { get; set; }
        /// <summary>
        /// How much money is desired to achieve this plan.
        /// </summary>
        public decimal PlannedMoney { get; set; }
        /// <summary>
        /// Planned type id
        /// </summary>
        public Guid PlannedTypeId { get; set; }
        /// <summary>
        /// Plan type name.
        /// </summary>
        public string PlannedTypeName { get; set; }
        /// <summary>
        /// Date when is the deadline of the plan.
        /// </summary>
        public DateTime? Deadline { get; set; }
        /// <summary>
        /// Date when is the plan starts
        /// </summary>
        public DateTime? Start { get; set; }
        /// <summary>
        /// States whether this plan is achieved.
        /// </summary>
        public bool IsCompleted { get; set; }
        /// <summary>
        /// Makes string representation of object based on its properties
        /// </summary>
        /// <returns>String representation of object</returns>
        public override string ToString()
        {
            return $"AccountId: {AccountId}, AccountName: {AccountName}, Description: {Description}, PlanType: {PlanType}, PlannedMoney: {PlannedMoney}, PlannedTypeId: {PlannedTypeId}, PlannedTypeName: {PlannedTypeName}, Deadline: {Deadline}, IsCompleted: {IsCompleted}";
        }
        /// <summary>
        /// Determites if two objects are the same one
        /// </summary>
        /// <param name="other">Object to be compared with</param>
        /// <returns>true if objects are same</returns>
        protected bool Equals(Plan other)
        {
            return AccountId == other.AccountId && string.Equals(AccountName, other.AccountName) && string.Equals(Description, other.Description) && PlanType == other.PlanType && PlannedMoney == other.PlannedMoney && PlannedTypeId == other.PlannedTypeId && string.Equals(PlannedTypeName, other.PlannedTypeName) && Deadline.Equals(other.Deadline) && IsCompleted == other.IsCompleted;
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
            return Equals((Plan) obj);
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
