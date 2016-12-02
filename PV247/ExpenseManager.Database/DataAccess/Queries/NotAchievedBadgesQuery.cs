using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Lists all not achieved badges for given account
    /// </summary>
    public class NotAchievedBadgesQuery : ExpenseManagerQuery<BadgeModel>
    {

        /// <summary>
        /// Id of account
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">unitOfWork provider</param>
        public NotAchievedBadgesQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
        protected override IQueryable<BadgeModel> GetQueryable()
        {
            return Context.Badges
                .Except(Context.AccountBadges
                            .Where(x => x.AccountId == AccountId)
                            .Select(x => x.Badge)
                );
        }
    }
}
