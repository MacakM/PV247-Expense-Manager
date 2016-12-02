using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostTypes
{
    internal class CostTypesByAccountId : IFilter<CostTypeModel>
    {
        /// <summary>
        /// Account id to be used in filter
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Filters query
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IQueryable<CostTypeModel> FilterQuery(IQueryable<CostTypeModel> queryable)
        {
            return queryable.Where(costType => costType.AccountId == AccountId);
        }
    }
}
