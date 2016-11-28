using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters cost info by its creation time
    /// </summary>
    public class CostInfosByCreatedFrom : Filter<CostInfo>
    {
        /// <summary>
        /// Left edge of created range
        /// </summary>
        public DateTime? CreatedFrom { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="start"></param>
        public CostInfosByCreatedFrom(DateTime? start)
        {
            CreatedFrom = start;
        }
    }
}
