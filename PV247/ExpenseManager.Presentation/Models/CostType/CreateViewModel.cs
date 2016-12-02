using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Presentation.Models.CostType
{
    /// <summary>
    /// Model for creating category
    /// </summary>
    public class CreateViewModel
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
