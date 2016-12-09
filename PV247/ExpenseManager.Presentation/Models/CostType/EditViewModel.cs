using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Presentation.Models.CostType
{
    /// <summary>
    /// View model for editing category
    /// </summary>
    public class EditViewModel
    {

        /// <summary>
        /// Id of category
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the category
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
