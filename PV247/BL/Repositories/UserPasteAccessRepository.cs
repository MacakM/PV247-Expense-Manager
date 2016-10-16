using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
    public class UserPasteAccessRepository : EntityFrameworkRepository<UserPasteAccess, int>
    {
        public UserPasteAccessRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
