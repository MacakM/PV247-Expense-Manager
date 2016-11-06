using System.Collections.Generic;
using System.Linq;
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
    // TODO doc
    /// <summary>
    /// 
    /// </summary>
    public class CostInfoService : ExpenseManagerQueryAndCrudServiceBase<CostInfoModel, int, ListCostInfosQuery, CostInfo, CostInfoModelFilter>, ICostInfoService
    {

        /// <summary>
        /// 
        /// </summary>
        protected override string[] EntityIncludes { get; } =
        {
            nameof(CostInfoModel.Account),
            nameof(CostInfoModel.Type)
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="repository"></param>
        /// <param name="expenseManagerMapper"></param>
        /// <param name="unitOfWorkProvider"></param>
        public CostInfoService(ListCostInfosQuery query, ExpenseManagerRepository<CostInfoModel, int> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costInfo"></param>
        public void CreateCostInfo(CostInfo costInfo)
        {
            // insert new CostType too?
            Save(costInfo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedCostInfo"></param>
        public void UpdateCostInfo(CostInfo updatedCostInfo)
        {
            Save(updatedCostInfo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costInfoId"></param>
        public void DeleteCostInfo(int costInfoId)
        {
            Delete(costInfoId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costInfoId"></param>
        /// <returns></returns>
        public CostInfo GetCostInfo(int costInfoId)
        {
            return GetDetail(costInfoId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<CostInfo> ListCostInfos(CostInfoFilter filter)
        {
            Query.Filter = Mapper.Map<CostInfoModelFilter>(filter);
            return GetList().ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        public void RecomputePeriodicCosts()
        {
            throw new System.NotImplementedException();
        }
    }
}
