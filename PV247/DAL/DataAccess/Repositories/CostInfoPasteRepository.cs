using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.DataAccess.Repositories
{
    public class CostInfoPasteRepository : ExpenseManagerRepository<CostInfoPaste, int>
    {
        public CostInfoPasteRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
