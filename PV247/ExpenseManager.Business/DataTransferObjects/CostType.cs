using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class CostType : BusinessObject<int>
    {
        /// <summary>
        /// Name of this type.
        /// </summary>
        [MaxLength(128)]
        public string Name { get; set; }
        /// <summary>
        /// All costs of this type.
        /// </summary>
        public List<CostInfo> CostInfoList { get; set; }
    }
}
