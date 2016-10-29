using APILayer.DTOs;
using AutoMapper;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for Badge entity.
    /// </summary>
    public class BadgeRepository : ExpenseManagerRepository<Badge, BadgeDTO, int>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        /// <param name="mapper">Mapper</param>
        public BadgeRepository(IUnitOfWorkProvider provider, Mapper mapper) : base(provider, mapper) { }
    }
}
