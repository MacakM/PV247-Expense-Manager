using APILayer.DTOs;
using AutoMapper;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for CostType entity.
    /// </summary>
    public class CostTypeRepository : ExpenseManagerRepository<CostType, CostTypeDTO, int>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        /// <param name="mapper">Mapper</param>
        public CostTypeRepository(IUnitOfWorkProvider provider, Mapper mapper) : base(provider, mapper) { }
    }
}
