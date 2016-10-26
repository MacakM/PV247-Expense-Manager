﻿using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Repositories
{
    public class PlanRepository : ExpenseManagerRepository<Plan, int>
    {
        public PlanRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
