using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Repositories
{
    public class CostTypeRepository : EntityFrameworkRepository<CostType, int>
    {
        public CostTypeRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
