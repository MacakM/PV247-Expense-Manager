using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.DataAccess.Repositories
{
    public class PasteRepository : ExpenseManagerRepository<Paste, int>
    {
        public PasteRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
