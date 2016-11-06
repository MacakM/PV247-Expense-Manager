﻿using System;
using ExpenseManager.Business.DataTransferObjects.Enums;

namespace ExpenseManager.Business.DataTransferObjects.Filters
{
    public class PlanFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public int? AccountId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? CostTypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CostTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool DoExactMatch { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// /
        /// </summary>
        public PlanType? PlanType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? PlannedMoneyFrom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? PlannedMoneyTo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DeadlineFrom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DeadlineTo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? IsCompleted { get; set; }
    }
}