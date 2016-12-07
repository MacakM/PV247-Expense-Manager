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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="balancesGroupedByDayQuery"></param>
        /// <param name="unitOfWorkProvider"></param>
        internal GraphService(ExpenseManagerQuery<DayBalance> balancesGroupedByDayQuery,
            IUnitOfWorkProvider unitOfWorkProvider)
        {
            _balancesGroupedByDayQuery = balancesGroupedByDayQuery as BalancesGroupedByDayQuery;
            _unitOfWorkProvider = unitOfWorkProvider;
        }

        /// <inheritdoc />
        public List<DayTotalBalance> GetTotalDailyBalanceGraphData(Guid accountId, decimal totalBalance)
        {
            _balancesGroupedByDayQuery.AccountId = accountId;
            using (_unitOfWorkProvider.Create())
            {
                var dailyBalances = _balancesGroupedByDayQuery.Execute();
                var dailyBalancesWithZeros = GetDailyBalancesWithZeros(dailyBalances);
                return CreateDailyTotalBalances(totalBalance, dailyBalancesWithZeros);
            }
        }

        // since not in all days there was some expense it would miss in the list
        // this method adds zeros to the list
        private List<DayBalance> GetDailyBalancesWithZeros(IList<DayBalance> dailyBalances)
        {
            var dictionaryBalances = new Dictionary<DateTime, decimal>();
            foreach (var dayBalance in dailyBalances)
            {
                dictionaryBalances.Add(dayBalance.Date, dayBalance.Balance);
            }

            var dailyBalancesWithZeros = new List<DayBalance>();
            for (int i = 9; i >= 0; i--)
            {
                var date = DateTime.UtcNow.AddDays(-i).Date;
                dailyBalancesWithZeros.Add(new DayBalance() {
                    Date = date,
                    Balance = dictionaryBalances.ContainsKey(date) ?
                                dictionaryBalances[date] : 0
                });
            }

            return dailyBalancesWithZeros;
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
