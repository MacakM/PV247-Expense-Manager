using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Repositories
{
    public class CostInfoPasteRepository : EntityFrameworkRepository<CostInfoPaste, int>
    {
        public CostInfoPasteRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
