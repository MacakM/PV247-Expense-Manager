using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Presentation.Models.Expense;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseManager.Presentation.Models.PermanentExpense
{
    /// <summary>
    /// Model for creating expenses
    /// </summary>
    public class CreatePermanentExpenseViewModel : CreateViewModel
    {

        /// <summary>
        /// Periodicty of expense
        /// </summary>
        [Required]
        public Periodicity Periodicity { get; set; }

        /// <summary>
        /// Mulptiplies periodicity
        /// </summary>
        [Required]
        [Range(0, int.MaxValue)]
        public int PeriodicMultiplicity { get; set; }

        /// <summary>
        /// Periodicities for select box
        /// </summary>
        public IEnumerable<SelectListItem> SelectPeriodicities
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Value = ((int) Periodicity.Day).ToString(),
                        Text = ExpenseManagerResource.Daily
                    },
                    new SelectListItem()
                    {
                        Value = ((int) Periodicity.Week).ToString(),
                        Text = ExpenseManagerResource.Weekly
                    },
                    new SelectListItem()
                    {
                        Value = ((int) Periodicity.Month).ToString(),
                        Text = ExpenseManagerResource.Monthly
                    }
                };
            }
        }
    }
}
