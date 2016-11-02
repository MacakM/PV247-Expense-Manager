﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ExpenseManager.Contract.DTOs;
using AutoMapper;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services
{
    public class PlanAndCrudService : ExpenseManagerQueryAndCrudServiceBase<Plan, int, IList<PlanDTO>, PlanDTO>
    {
        public PlanAndCrudService(IQuery<IList<PlanDTO>> query, IRepository<Plan, PlanDTO, int> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider)
            : base(query, repository, expenseManagerMapper, unitOfWorkProvider) { }

        public void CreatePlan(PlanDTO planDTO)
        {
            Save(planDTO);
        }

        // TODO add more functionality


        protected override string[] EntityIncludes => new string[]
        {
            
        };
    }
}
