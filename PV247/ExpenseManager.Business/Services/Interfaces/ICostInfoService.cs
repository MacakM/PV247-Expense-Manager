using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    public interface ICostInfoService
    {
        void CreateCostInfo(CostInfo costInfo);
        void UpdateCostInfo(CostInfo updatedCostInfo);
        void DeleteCostInfo(int costInfoId);
        CostInfo GetCostInfo(int costInfoId);
        List<CostInfo> ListCostInfos(CostInfoFilter filter);

    }
}
