using System;
using System.Data.Entity;
using System.Linq;
using ExpenseManager.Database.Enums;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Query for retrieving balance for each of last 10 days
    /// </summary>
    public class BalancesGroupedByDayQuery : ExpenseManagerQuery<DayBalance>
    {

        /// <summary>
        /// Id of the account for which to execute query
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="provider"></param>
        public BalancesGroupedByDayQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        /// <summary>
        /// Returns IQueryable
        /// </summary>
        /// <returns></returns>
        protected override IQueryable<DayBalance> GetQueryable()
        {
            var fromDate = DateTime.UtcNow.AddDays(-10);
            return  from x in Context.CostInfos
                    let dt = DbFunctions.TruncateTime(x.Created)
                    where x.AccountId == AccountId
                    where x.Created > fromDate
                    where x.Periodicity == PeriodicityModel.None
                    group x by dt into g
                    select new DayBalance()
                    {
                        Balance = (g.Count(y => y.IsIncome) == 0 ? 0 : g.Where(y => y.IsIncome).Sum(y => y.Money) )
                                   - (g.Count(y => !y.IsIncome) == 0 ? 0 : g.Where(y => !y.IsIncome).Sum(y => y.Money)),
                        Date = g.Key.Value
                    };
        }
    }

    /// <summary>
    /// Holds balance for one day
    /// </summary>
    public class DayBalance
    {
        /// <summary>
        /// represents day
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// represents balance in given day
        /// </summary>
        public decimal Balance { get; set; }
    }
}
