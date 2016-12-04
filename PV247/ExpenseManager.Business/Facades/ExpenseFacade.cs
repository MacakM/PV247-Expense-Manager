using System;
using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.DataTransferObjects.Factories;
using ExpenseManager.Business.Infrastructure.CastleWindsor;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.Facades
{
    /// <summary>
    /// Handles operation about cost information and its types.
    /// </summary>
    public class ExpenseFacade
    {
        private readonly ICostInfoService _costInfoService = BusinessLayerDIManager.Resolve<ICostInfoService>();

        private readonly ICostTypeService _costTypeService = BusinessLayerDIManager.Resolve<ICostTypeService>();
        
        /// <summary>
        /// Creates new cost info object in databse
        /// </summary>
        /// <param name="costInfo"></param>
        public Guid CreateItem(CostInfo costInfo)
        {
            return _costInfoService.CreateCostInfo(costInfo);
        }

        /// <summary>
        /// Deletes cost info specified by cost info
        /// </summary>
        /// <param name="costInfoId"></param>
        public void DeleteItem(Guid costInfoId)
        {
            _costInfoService.DeleteCostInfo(costInfoId);
        }

        /// <summary>
        /// Updates existing cost info
        /// </summary>
        /// <param name="updatedCostInfo">updated cost info</param>
        public void UpdateItem(CostInfo updatedCostInfo)
        {
            _costInfoService.UpdateCostInfo(updatedCostInfo);
        }

        /// <summary>
        /// Get cost info specified by unique id
        /// </summary>
        /// <param name="costInfoId">Unique id</param>
        /// <returns>Cost info</returns>
        public CostInfo GetItem(Guid costInfoId)
        {
            return _costInfoService.GetCostInfo(costInfoId);
        }

        /// <summary>
        /// List cost infos
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<CostInfo> ListItems(Guid typeId, PageInfo pageInfo)
        {
            return ListItems(null, null, null, null, null, null, typeId, null, pageInfo);
        }

        /// <summary>
        /// List cost infos
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="periodicity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<CostInfo> ListItems(Guid? accountId, Periodicity? periodicity, PageInfo pageInfo)
        {
            return ListItems(accountId, periodicity, null, null, null, null, null, null, pageInfo);
        }

        /// <summary>
        /// List cost types based on filter
        /// </summary>
        /// <param name="periodicity"></param>
        /// <param name="isIncome"></param>
        /// <param name="accountId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="moneyFrom"></param>
        /// <param name="moneyTo"></param>
        /// <param name="costTypeId"></param>
        /// <param name="pageInfo"></param>
        /// <returns>List of cost infos</returns>
        public List<CostInfo> ListItems(Guid? accountId, Periodicity? periodicity, DateTime? dateFrom, DateTime? dateTo, decimal? moneyFrom, decimal? moneyTo, Guid? costTypeId, bool? isIncome, PageInfo pageInfo)
        {
            var filters = FilterFactory.GetCostItemsFilters(accountId, periodicity, dateFrom, dateTo, moneyFrom, moneyTo, costTypeId, isIncome);
            return _costInfoService.ListCostInfos(filters, FilterFactory.GetPageAndOrderable<CostInfoModel>(pageInfo));
        }

        /// <summary>
        /// Gets the count of rows in database filtered by filter
        /// Used for pagination
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="periodicity"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="moneyFrom"></param>
        /// <param name="moneyTo"></param>
        /// <param name="costTypeId"></param>
        /// <param name="isIncome"></param>
        /// <returns></returns>
        public int GetCostInfosCount(Guid? accountId, Periodicity? periodicity, DateTime? dateFrom, DateTime? dateTo, decimal? moneyFrom, decimal? moneyTo, Guid? costTypeId, bool? isIncome)
        {
            var filters = FilterFactory.GetCostItemsFilters(accountId, periodicity, dateFrom, dateTo, moneyFrom, moneyTo, costTypeId, isIncome);
            return _costInfoService.GetCostInfosCount(filters, null);
        }

        /// <summary>
        /// Creaates new cost type
        /// </summary>
        /// <param name="costType">Object to be added to database</param>
        public void CreateItemType(CostType costType)
        {
            _costTypeService.CreateCostType(costType);
        }

        /// <summary>
        /// Deletes cost type specified by id
        /// </summary>
        /// <param name="costTypeId">Unique cost type id</param>
        public void DeleteItemType(Guid costTypeId)
        {
            _costTypeService.DeleteCostType(costTypeId);
        }

        /// <summary>
        /// Updates existing cost type
        /// </summary>
        /// <param name="updatedCostType">Modified existing cost type</param>
        public void UpdateItemType(CostType updatedCostType)
        {
            _costTypeService.UpdateCostType(updatedCostType);
        }

        /// <summary>
        /// Get cost type specified by unique id
        /// </summary>
        /// <param name="itemTypeId">Unique cost type id</param>
        /// <returns></returns>
        public CostType GetItemType(Guid itemTypeId)
        {
            return _costTypeService.GetCostType(itemTypeId);
        }

        /// <summary>
        /// Lists all cost types for given account id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<CostType> ListItemTypes(Guid accountId)
        {
            var filters = FilterFactory.GetCostTypeFilters(accountId);
            return _costTypeService.ListCostTypes(filters, null);
        }

        /// <summary>
        /// List cost types specified by filter
        /// </summary>
        /// <param name="costTypeName"></param>
        /// <param name="pageInfo"></param>
        /// <returns>List of cost typer</returns>
        public List<CostType> ListItemTypes(string costTypeName, PageInfo pageInfo)
        {
            var filters = FilterFactory.GetCostTypeFilters(costTypeName);
            return _costTypeService.ListCostTypes(filters, FilterFactory.GetPageAndOrderable<CostTypeModel>(pageInfo));
        }
    }
}