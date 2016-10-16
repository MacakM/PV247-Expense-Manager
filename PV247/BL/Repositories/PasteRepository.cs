using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
    public class PasteRepository : EntityFrameworkRepository<Paste, int>
    {
        public PasteRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
