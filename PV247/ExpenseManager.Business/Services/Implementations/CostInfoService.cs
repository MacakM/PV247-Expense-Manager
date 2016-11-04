using System.Collections.Generic;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    public class CostInfoService : ExpenseManagerQueryAndCrudServiceBase<CostInfoModel, int, IList<CostInfo>, CostInfo>, ICostInfoService
    {
        private readonly CostInfoRepository _costInfoRepository;

        public CostInfoService(IQuery<IList<CostInfo>> query,
            ExpenseManagerRepository<CostInfoModel, int> repository, Mapper expenseManagerMapper,
            IUnitOfWorkProvider unitOfWorkProvider, CostInfoRepository costInfoRepository)
            : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
            _costInfoRepository = costInfoRepository;
        }
       
        public void CreateCost(CostInfo cost)
        {
            // insert new CostType too?
            Save(cost);
        }

        public void DeleteCost(int costId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                _costInfoRepository.Delete(costId);
                uow.Commit();
            }
        }

        public CostInfo GetCost(int costId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var plan = _costInfoRepository.GetById(costId);
                return plan != null ? Mapper.Map<CostInfo>(plan) : null;
            }
        }

        protected override string[] EntityIncludes { get; } =
        {
            nameof(CostInfoModel.Account),
            nameof(CostInfoModel.Type)
        };
    }
}
