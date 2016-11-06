using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// Service handles AccountBadge entity operations
    /// </summary>
    public class CostTypeService : ExpenseManagerQueryAndCrudServiceBase<CostTypeModel, int, CostType, CostTypeModelFilter>, ICostTypeService
    {
   
        /// <summary>
        /// 
        /// </summary>
        protected override string[] EntityIncludes { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="repository"></param>
        /// <param name="expenseManagerMapper"></param>
        /// <param name="unitOfWorkProvider"></param>
        public CostTypeService(ExpenseManagerQuery<CostTypeModel, CostTypeModelFilter> query, ExpenseManagerRepository<CostTypeModel, int> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
        }
        /// <summary>
        /// Creaates new cost type
        /// </summary>
        /// <param name="costType">Object to be added to database</param>
        public void CreateCostType(CostType costType)
        {
            Save(costType);
        }
        /// <summary>
        /// Updates existing cost type
        /// </summary>
        /// <param name="costType">Modified existing cost type</param>
        public void UpdateCostType(CostType costType)
        {
           Save(costType);
        }
        /// <summary>
        /// Deletes cost type specified by id
        /// </summary>
        /// <param name="costTypeId">Unique cost type id</param>
        public void DeleteCostType(int costTypeId)
        {
           Delete(costTypeId);
        }
        /// <summary>
        /// Get cost type specified by unique id
        /// </summary>
        /// <param name="costTypeId">Unique cost type id</param>
        /// <returns></returns>
        public CostType GetCostType(int costTypeId)
        {
            return GetDetail(costTypeId);
        }
        /// <summary>
        /// List cost types specified by filter
        /// </summary>
        /// <param name="filter">Filters cost types</param>
        /// <returns>List of cost typer</returns>
        public List<CostType> ListCostTypes(CostTypeFilter filter)
        {
            Query.Filter = ExpenseManagerMapper.Map<CostTypeModelFilter>(filter);
            return GetList().ToList();
        }


    
    }
}
