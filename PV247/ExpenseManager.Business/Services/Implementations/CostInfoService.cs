using System.Collections.Generic;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
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
       
        public void CreateCostInfo(CostInfo costInfo)
        {
            // insert new CostType too?
            Save(costInfo);
        }

        public void UpdateCostInfo(CostInfo updatedCostInfo)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCostInfo(int costInfoId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                _costInfoRepository.Delete(costInfoId);
                uow.Commit();
            }
        }

        public CostInfo GetCostInfo(int costInfoId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var plan = _costInfoRepository.GetById(costInfoId);
                return plan != null ? Mapper.Map<CostInfo>(plan) : null;
            }
        }

        public List<CostInfo> ListCostInfos(CostInfoFilter filter)
        {
            throw new System.NotImplementedException();
        }

        protected override string[] EntityIncludes { get; } =
        {
            nameof(CostInfoModel.Account),
            nameof(CostInfoModel.Type)
        };
    }
}
