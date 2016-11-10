using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Presentation.Models.CostType
{
    /// <summary>
    /// Presentation layer representation of CostTypeModel object
    /// </summary>
    public class IndexViewModel : ViewModelId
    {
        /// <summary>
        /// Name of this type.
        /// </summary>
        public string Name { get; set; }
    }
}
