using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Repositories
{
    public class CostInfoRepository : EntityFrameworkRepository<CostInfo, int>
    {
        public CostInfoRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
