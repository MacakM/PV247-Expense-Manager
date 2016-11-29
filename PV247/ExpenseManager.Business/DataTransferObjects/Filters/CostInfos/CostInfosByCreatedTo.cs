﻿using System;
using System.Linq;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters by time of cost info creation
    /// </summary>
    public class CostInfosByCreatedTo : IFilter<CostInfoModel>
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

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostInfoModel> FilterQuery(IQueryable<CostInfoModel> queryable)
        {
            return queryable.Where(costInfo => costInfo.Created <= CreatedTo);
        }
    }
}
