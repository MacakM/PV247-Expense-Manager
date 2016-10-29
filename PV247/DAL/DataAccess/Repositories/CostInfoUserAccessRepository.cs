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
    public class CostInfoUserAccessRepository : ExpenseManagerRepository<CostInfoUserAccess, CostInfoUserAccessDTO, int>
    {
        public CostInfoUserAccessRepository(IUnitOfWorkProvider provider, Mapper mapper) : base(provider, mapper) { }
    }
}
