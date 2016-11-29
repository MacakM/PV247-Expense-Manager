using System;
using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;

namespace ExpenseManager.Business.Services.Interfaces
{
    /// <summary>
    /// Service providing data for graphs
    /// </summary>
    public interface IGraphService : IService
    {
        /// <summary>
        /// Gets balances for each of last 10 days
        /// </summary>
        /// <param name="accountId"></param>
        List<DayTotalBalance> GetTotalDailyBalanceGraphData(Guid accountId);
    }
}
