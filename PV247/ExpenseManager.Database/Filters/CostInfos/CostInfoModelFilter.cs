using System;
using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Database.Filters.CostInfos
{
    /// <summary>
    /// Filter userd in queries in order to get cost infos with specifies parameters
    /// </summary>
    public class CostInfoModelFilterModel : IFilterModel<CostInfoModel>
    {
        /// <summary>
        /// Filter of income if false, do not filter if is null
        /// </summary>
        public bool? IsIncome { get; set; }

        /// <summary>
        /// Left edge of money range
        /// </summary>
        public decimal? MoneyFrom { get; set; }

        /// <summary>
        /// Right edge of money range
        /// </summary>
        public decimal? MoneyTo { get; set; }

        /// <summary>
        /// Account id to be filtered with
        /// </summary>
        public Guid? AccountId { get; set; }

        /// <summary>
        /// Account name to be filtered with
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Determines if Equals() or Contains() should be used when matching string parameters
        /// </summary>
        public bool DoExactMatch { get; set; }

        /// <summary>
        /// Left edge of created range
        /// </summary>
        public DateTime? CreatedFrom { get; set; }

        /// <summary>
        /// Right edge of created range
        /// </summary>
        public DateTime? CreatedTo { get; set; }

        /// <summary>
        /// Type id to be filtered with
        /// </summary>
        public Guid? TypeId { get; set; }

        /// <summary>
        /// Type name to be 
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Periodicity of cost
        /// </summary>
        public PeriodicityModel? Periodicity { get; set; }

        /// <summary>
        /// Mulptiplies periodicity
        /// </summary>
        public int? PeriodicMultiplicityFrom { get; set; }

        /// <summary>
        /// Mulptiplies periodicity
        /// </summary>
        public int? PeriodicMultiplicityTo { get; set; }

        /// <summary>
        /// Filters given query
        /// </summary>
        /// <param name="queryable">Query to be filtered</param>
        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            if (!string.IsNullOrEmpty(AccountName))
            {
                queryable = DoExactMatch ? queryable.Where(costInfo => costInfo.Account.Name.Equals(AccountName)) : queryable.Where(costInfo => costInfo.Account.Name.Contains(AccountName));
            }
            if (!string.IsNullOrEmpty(TypeName))
            {
                queryable = DoExactMatch ? queryable.Where(costInfo => costInfo.Type.Name.Equals(TypeName)) : queryable.Where(costInfo => costInfo.Type.Name.Contains(TypeName));
            }
            if (AccountId != null)
            {
                queryable = queryable.Where(costInfo => costInfo.AccountId == AccountId.Value);
            }
            if (IsIncome != null)
            {
                queryable = queryable.Where(costInfo => costInfo.IsIncome == IsIncome.Value);
            }
            if (Periodicity != null)
            {
                queryable = queryable.Where(costInfo => costInfo.Periodicity == Periodicity.Value);
            }
            if (PeriodicMultiplicityFrom != null)
            {
                queryable =
                    queryable.Where(
                        costInfo => costInfo.PeriodicMultiplicity.Value >= PeriodicMultiplicityFrom.Value);
            }
            if (PeriodicMultiplicityTo != null)
            {
                queryable =
                    queryable.Where(
                        costInfo => costInfo.PeriodicMultiplicity.Value <= PeriodicMultiplicityTo.Value);
            }
            if (TypeId != null)
            {
                queryable = queryable.Where(costInfo => costInfo.TypeId == TypeId.Value);
            }
            if (CreatedFrom != null)
            {
                queryable = queryable.Where(costInfo => costInfo.Created >= CreatedFrom.Value);
            }
            if (CreatedTo != null)
            {
                queryable = queryable.Where(costInfo => costInfo.Created <= CreatedTo.Value);
            }
            if (MoneyFrom != null)
            {
                queryable = queryable.Where(costInfo => costInfo.Money >= MoneyFrom.Value);
            }
            if (MoneyTo != null)
            {
                queryable = queryable.Where(costInfo => costInfo.Money <= MoneyTo.Value);
            }
            return queryable;
        }
    }
}
