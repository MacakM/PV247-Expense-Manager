using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APILayer.DTOs;
using AutoMapper;
using DAL.Entities;
using DAL.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for CostInfoUserAccess entity.
    /// </summary>
    public class CostInfoUserAccessRepository : ExpenseManagerRepository<CostInfoUserAccess, CostInfoUserAccessDTO, int>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        /// <param name="mapper">Mapper</param>
        public CostInfoUserAccessRepository(IUnitOfWorkProvider provider, Mapper mapper) : base(provider, mapper) { }
    }
}
