using System.Collections.Generic;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    
    public class CostTypeService : ExpenseManagerQueryAndCrudServiceBase<CostTypeModel, int, ListCostTypesQuery, CostType, CostTypeModelFilter>, ICostTypeService
    {
        protected override string[] EntityIncludes { get; }

        public CostTypeService(ListCostTypesQuery query, ExpenseManagerRepository<CostTypeModel, int> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
        }
        public void CreateCostType(CostType costType)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCostType(CostType costType)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCostType(int costTypeId)
        {
            throw new System.NotImplementedException();
        }

        public Plan GetCostType(int costTypeId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CostType> ListCostTypes(CostTypeFilter filter)
        {
            throw new System.NotImplementedException();
        }


    
    }
}
