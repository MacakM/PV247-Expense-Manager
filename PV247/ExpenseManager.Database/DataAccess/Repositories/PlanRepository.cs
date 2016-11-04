using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for Plan entity.
    /// </summary>
    public class PlanRepository : ExpenseManagerRepository<PlanModel, int>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        public PlanRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
