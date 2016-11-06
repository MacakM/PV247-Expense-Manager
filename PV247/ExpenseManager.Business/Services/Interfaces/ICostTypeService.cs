using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    public interface ICostTypeService
    {
        void CreateCostType(CostType costType);
        void UpdateCostType(CostType costType);
        void DeleteCostType(int costTypeId);
        Plan GetCostType(int costTypeId);
        IEnumerable<CostType> ListCostTypes(CostTypeFilter filter);
    }
}
