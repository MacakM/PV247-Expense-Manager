using System.ComponentModel.DataAnnotations;

namespace DAL.DTOs
{
    public class PlanDTO : ExpenseManagerDTO<int>
    {
        /// <summary>
        /// User that created this plan.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Description of the plan.
        /// </summary>
        [MaxLength(256)]
        public string Description { get; set; }

        /// <summary>
        /// How much money is needed to achieve this plan.
        /// </summary>
        public int PlannedMoney { get; set; }

        /// <summary>
        /// Which type of cost is assigned to this plan.
        /// </summary>
        public CostTypeDTO PlannedType { get; set; }

        /// <summary>
        /// States whether this plan is achieved.
        /// </summary>
        public bool IsCompleted { get; set; }

        protected bool Equals(PlanDTO other)
        {
            return Id == other.Id && UserId == other.UserId && string.Equals(Description, other.Description) && PlannedMoney == other.PlannedMoney && Equals(PlannedType, other.PlannedType) && IsCompleted == other.IsCompleted;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PlanDTO)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ UserId;
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ PlannedMoney;
                hashCode = (hashCode * 397) ^ (PlannedType != null ? PlannedType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsCompleted.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"UserId: {UserId}, Description: {Description}, PlannedMoney: {PlannedMoney}, IsCompleted: {IsCompleted}";
        }
    }
}
