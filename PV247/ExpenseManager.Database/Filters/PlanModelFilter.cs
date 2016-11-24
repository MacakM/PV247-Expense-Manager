using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Database.Filters
{
    /// <summary>
    /// Filter userd in queries in order to get plans with specifies parameters
    /// </summary>
    public class PlanModelFilter : FilterModelBase<PlanModel>
    {
        /// <summary>
        /// Account id to be used in filter
        /// </summary>
        public Guid? AccountId { get; set; }
        /// <summary>
        /// Account name to be used in filter
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Cost type id to be used in filter
        /// </summary>
        public Guid? CostTypeId { get; set; }
        /// <summary>
        /// Cost type name to be used in filter
        /// </summary>
        public string CostTypeName { get; set; }
        /// <summary>
        /// Determines if query shuold 
        /// </summary>
        public bool DoExactMatch { get; set; }
        /// <summary>
        /// Description of plan to be used in filter
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Plan type to be used in filter
        /// </summary>
        public PlanTypeModel? PlanType { get; set; }
        /// <summary>
        /// Left edge of planned money range
        /// </summary>
        public decimal? PlannedMoneyFrom { get; set; }
        /// <summary>
        /// Right edge of planned money range
        /// </summary>
        public decimal? PlannedMoneyTo { get; set; }
        /// <summary>
        /// Left edge of deadline range
        /// </summary>
        public DateTime? DeadlineFrom { get; set; }
        /// <summary>
        /// Right edge of deadline range
        /// </summary>
        public DateTime? DeadlineTo { get; set; }
        /// <summary>
        /// Date when is the plan starts
        /// </summary>
        public DateTime? StartFrom { get; set; }
        /// <summary>
        /// Date when is the plan starts
        /// </summary>
        public DateTime? StartTo { get; set; }
        /// <summary>
        /// If plan is completed
        /// </summary>
        public bool? IsCompleted { get; set; }

        /// <summary>
        /// Filters given query
        /// </summary>
        /// <param name="queryable">Query to be filtered</param>
        public override IQueryable<PlanModel> FilterQuery(IQueryable<PlanModel> queryable)
        {
            if (!string.IsNullOrEmpty(AccountName))
            {
                queryable = DoExactMatch ? queryable.Where(plan => plan.Account.Name.Equals(AccountName)) : queryable.Where(plan => plan.Account.Name.Contains(AccountName));
            }
            if (!string.IsNullOrEmpty(CostTypeName))
            {
                queryable = DoExactMatch ? queryable.Where(plan => plan.PlannedType.Name.Equals(CostTypeName)) : queryable.Where(plan => plan.PlannedType.Name.Equals(CostTypeName));
            }
            if (!string.IsNullOrEmpty(Description))
            {
                queryable = DoExactMatch ? queryable.Where(plan => plan.Description.Equals(Description)) : queryable.Where(plan => plan.Description.Contains(Description));
            }
            if (CostTypeId != null)
            {
                queryable = queryable.Where(plan => plan.PlannedType.Id == CostTypeId.Value);
            }
            if (AccountId != null)
            {
                queryable = queryable.Where(plan => plan.AccountId == AccountId.Value);
            }
            if (PlanType != null)
            {
                queryable = queryable.Where(plan => plan.PlanType == PlanType.Value);
            }
            if (IsCompleted != null)
            {
                queryable = queryable.Where(plan => plan.IsCompleted == IsCompleted.Value);
            }
            if (DeadlineFrom != null)
            {
                queryable = queryable.Where(plan => plan.Deadline >= DeadlineFrom.Value);
            }
            if (DeadlineTo != null)
            {
                queryable = queryable.Where(plan => plan.Deadline <= DeadlineTo.Value);
            }
            if (StartFrom != null)
            {
                queryable = queryable.Where(plan => plan.Start.Value >= StartFrom.Value);
            }
            if (StartTo != null)
            {
                queryable = queryable.Where(plan => plan.Start.Value <= StartTo.Value);
            }
            if (PlannedMoneyFrom != null)
            {
                queryable = queryable.Where(plan => plan.PlannedMoney >= PlannedMoneyFrom.Value);
            }
            if (PlannedMoneyTo != null)
            {
                queryable = queryable.Where(plan => plan.PlannedMoney <= PlannedMoneyTo.Value);
            }
            if (OrderByDesc == null || string.IsNullOrEmpty(OrderByPropertyName))
            {
                return queryable;
            }
            System.Reflection.PropertyInfo prop = typeof(PlanModel).GetProperty(OrderByPropertyName);
            if (prop == null)
            {
                return queryable;
            }
            queryable = OrderByDesc.Value ? QueryOrderByHelper.OrderByDesc(queryable, OrderByPropertyName) : QueryOrderByHelper.OrderBy(queryable, OrderByPropertyName);
            if (PageNumber != null)
            {
                queryable = queryable.Skip(Math.Max(0, PageNumber.Value - 1) * PageSize);
            }
            return queryable.Take(PageSize);
        }
    }
}
