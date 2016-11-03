using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseManager.Business.DTOs;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    public class CostInfoAndCrudService : ExpenseManagerQueryAndCrudServiceBase<CostInfo, int, IList<CostInfoDTO>, CostInfoDTO>, ICostInfoAndCrudService
    {
        private readonly CostInfoRepository _costInfoRepository;

        public CostInfoAndCrudService(IQuery<IList<CostInfoDTO>> query,
            ExpenseManagerRepository<CostInfo, int> repository, Mapper expenseManagerMapper,
            IUnitOfWorkProvider unitOfWorkProvider, CostInfoRepository costInfoRepository)
            : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
            _costInfoRepository = costInfoRepository;
        }

        protected override Expression<Func<CostInfoDTO, object>>[] EntityIncludes { get; }
        public void CreateCost(CostInfoDTO costDto)
        {
            // insert new CostType too?
            Save(costDto);
        }

        public void DeleteCost(int costId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                _costInfoRepository.Delete(costId);
                uow.Commit();
            }
        }

        public CostInfoDTO GetCost(int costId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var plan = _costInfoRepository.GetById(costId);
                return plan != null ? Mapper.Map<CostInfoDTO>(plan) : null;
            }
        }

        
    }
}
