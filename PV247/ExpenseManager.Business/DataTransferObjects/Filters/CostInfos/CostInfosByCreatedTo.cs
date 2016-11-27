using System;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters by time of cost info creation
    /// </summary>
    public class CostInfosByCreatedTo : Filter<CostInfo>
    {
        /// <summary>
        /// Right edge of created range
        /// </summary>
        public DateTime CreatedTo { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="createdTo"></param>
        public CostInfosByCreatedTo(DateTime createdTo)
        {
            CreatedTo = createdTo;
        }
    }
}
