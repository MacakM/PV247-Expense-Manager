using ExpenseManager.Business.DataTransferObjects;

namespace ExpenseManager.Business.Services.Interfaces
{
    public interface ICostInfoService
    {
        void CreateCost(CostInfo cost);
        void DeleteCost(int costId);
        CostInfo GetCost(int costId);
    }
}
