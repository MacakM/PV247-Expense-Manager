using APILayer.DTOs;
using AutoMapper;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for CostInfo entity.
    /// </summary>
    public class CostInfoRepository : ExpenseManagerRepository<CostInfo, CostInfoDTO, int>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        /// <param name="mapper">Mapper</param>
        public CostInfoRepository(IUnitOfWorkProvider provider, Mapper mapper) : base(provider, mapper) { }
    }
}
