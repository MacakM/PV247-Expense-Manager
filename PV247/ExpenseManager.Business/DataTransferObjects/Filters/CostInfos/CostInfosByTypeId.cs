using System;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters by planned type id
    /// </summary>
    public class CostInfosByTypeId : IFilter<CostInfo>
    {
        /// <summary>
        /// Type id to be filtered with
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// Filter construcotr
        /// </summary>
        /// <param name="plannedTypeId"></param>
        public CostInfosByTypeId(Guid plannedTypeId)
        {
            TypeId = plannedTypeId;
        }
    }
}
