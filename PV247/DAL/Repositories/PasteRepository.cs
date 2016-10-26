using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Repositories
{
    public class PasteRepository : EntityFrameworkRepository<Paste, int>
    {
        public PasteRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
