using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Business.DTOs;

namespace ExpenseManager.Business.Services.Interfaces
{
    public interface ICostInfoAndCrudService
    {
        void CreateCost(CostInfoDTO costDto);
        void DeleteCost(int costId);
        CostInfoDTO GetCost(int costId);
    }
}
