using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// Service providing data for graphs
    /// </summary>
    internal class GraphService : IGraphService
    {
        private readonly BalancesGroupedByDayQuery _balancesGroupedByDayQuery;
        private readonly IUnitOfWorkProvider _unitOfWorkProvider;
        private readonly ICostInfoService _costInfoService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="balancesGroupedByDayQuery"></param>
        /// <param name="unitOfWorkProvider"></param>
        /// <param name="costInfoService"></param>
        internal GraphService(ExpenseManagerQuery<DayBalance> balancesGroupedByDayQuery,
            IUnitOfWorkProvider unitOfWorkProvider,
            ICostInfoService costInfoService)
        {
            _balancesGroupedByDayQuery = balancesGroupedByDayQuery as BalancesGroupedByDayQuery;
            _unitOfWorkProvider = unitOfWorkProvider;
            _costInfoService = costInfoService;
        }

        /// <inheritdoc />
        public List<DayTotalBalance> GetTotalDailyBalanceGraphData(Guid accountId)
        {
            _balancesGroupedByDayQuery.AccountId = accountId;
            using (_unitOfWorkProvider.Create())
            {
                var totalBalance = _costInfoService.GetBalance(accountId);
                var dailyBalances = _balancesGroupedByDayQuery.Execute();
                return CreateDailyTotalBalances(totalBalance, dailyBalances);
            }
        }

        private List<DayTotalBalance> CreateDailyTotalBalances(decimal totalBalance, IList<DayBalance> dailyBalances)
        {
            var dayTotalBalanceList = new List<DayTotalBalance>();
            decimal accumulatedHistoryBalance = 0;
            foreach (var dayBalance in dailyBalances.Reverse())
            {
                var dayTotalBalance = new DayTotalBalance()
                {
                    Date = dayBalance.Date,
                    TotalBalance = totalBalance - accumulatedHistoryBalance
                };
                dayTotalBalanceList.Insert(0, dayTotalBalance);

                accumulatedHistoryBalance += dayBalance.Balance;
            }

            return dayTotalBalanceList;
        }
    }
}
