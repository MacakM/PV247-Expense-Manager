using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.Infrastructure;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.FilterInterfaces;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Query;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// Service handles AccountBadge entity operations
    /// </summary>
    public class CostTypeService : ExpenseManagerQueryAndCrudServiceBase<CostTypeModel, Guid, CostType>, ICostTypeService
    {
        /// <summary>
        /// Entity includes
        /// </summary>
        protected override string[] EntityIncludes { get; } = {nameof(CostType.CostInfoList) };

        /// <summary>
        /// Cost type service constructor
        /// </summary>
        /// <param name="query">Query</param>
        /// <param name="repository">Repository</param>
        /// <param name="expenseManagerMapper">Mapper</param>
        /// <param name="unitOfWorkProvider">Unit of work provider</param>
        public CostTypeService(ExpenseManagerQuery<CostTypeModel> query, ExpenseManagerRepository<CostTypeModel, Guid> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(query, repository, expenseManagerMapper, unitOfWorkProvider)
        {
        }

        /// <summary>
        /// Creaates new cost type
        /// </summary>
        /// <param name="costType">Object to be added to database</param>
        public void CreateCostType(CostType costType)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Save(costType);
                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Updates existing cost type
        /// </summary>
        /// <param name="costType">Modified existing cost type</param>
        public void UpdateCostType(CostType costType)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Save(costType);
                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Deletes cost type specified by id
        /// </summary>
        /// <param name="costTypeId">Unique cost type id</param>
        public void DeleteCostType(Guid costTypeId)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Delete(costTypeId);
                unitOfWork.Commit();
            }           
        }

        /// <summary>
        /// Get cost type specified by unique id
        /// </summary>
        /// <param name="costTypeId">Unique cost type id</param>
        /// <returns></returns>
        public CostType GetCostType(Guid costTypeId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return GetDetail(costTypeId);
            }          
        }

        /// <summary>
        /// List cost types specified by filter
        /// </summary>
        /// <param name="filters">Filters cost types</param>
        /// <param name="pageAndOrder"></param>
        /// <returns>List of cost typer</returns>
        public List<CostType> ListCostTypes(List<IFilter<CostTypeModel>> filters, IPageAndOrderable<CostTypeModel> pageAndOrder)
        {
            Query.Filters = filters;
            Query.PageAndOrderModelFilterModel = pageAndOrder;
            using (UnitOfWorkProvider.Create())
            {
                return GetList().ToList();
            }           
        }
    }
}
