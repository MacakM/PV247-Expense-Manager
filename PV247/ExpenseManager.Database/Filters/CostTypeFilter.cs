using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Filters
{
    public class CostTypeFilter : FilterBase
    {
        /// <summary>
        /// Id of the cost type.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of this type.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// All costs of this type.
        /// </summary>
        public virtual List<CostInfoModel> CostInfoList { get; set; }
    }
}
