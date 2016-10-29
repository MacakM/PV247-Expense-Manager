using APILayer.DTOs;
using AutoMapper;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for UserBadge entity.
    /// </summary>
    public class UserBadgeRepository : ExpenseManagerRepository<UserBadge, UserBadgeDTO, int>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        /// <param name="mapper">Mapper</param>
        public UserBadgeRepository(IUnitOfWorkProvider provider, Mapper mapper) : base(provider, mapper) { }
    }
}
