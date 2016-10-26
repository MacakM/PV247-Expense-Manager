using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.DataAccess.Repositories
{
    public class CostInfoRepository : ExpenseManagerRepository<CostInfo, int>
    {
        public CostInfoRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
