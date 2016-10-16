using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
    public class CostInfoRepository : EntityFrameworkRepository<CostInfo, int>
    {
        public CostInfoRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
