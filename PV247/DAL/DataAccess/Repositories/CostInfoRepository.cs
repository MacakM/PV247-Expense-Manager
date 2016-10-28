﻿using APILayer.DTOs;
using AutoMapper;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.DataAccess.Repositories
{
    public class CostInfoRepository : ExpenseManagerRepository<CostInfo, CostInfoDTO, int>
    {
        public CostInfoRepository(IUnitOfWorkProvider provider, Mapper mapper) : base(provider, mapper) { }
    }
}