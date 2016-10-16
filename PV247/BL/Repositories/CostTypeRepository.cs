using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
    public class CostTypeRepository : EntityFrameworkRepository<CostType, int>
    {
        public CostTypeRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
