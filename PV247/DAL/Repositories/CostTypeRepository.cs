using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Repositories
{
    public class CostTypeRepository : ExpenseManagerRepository<CostType, int>
    {
        public CostTypeRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
