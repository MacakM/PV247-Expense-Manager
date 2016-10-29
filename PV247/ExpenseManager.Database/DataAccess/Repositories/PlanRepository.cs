using ExpenseManager.Contract.DTOs;
using AutoMapper;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for Plan entity.
    /// </summary>
    public class PlanRepository : ExpenseManagerRepository<Plan, PlanDTO, int>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        /// <param name="mapper">Mapper</param>
        public PlanRepository(IUnitOfWorkProvider provider, Mapper mapper) : base(provider, mapper) { }
    }
}
