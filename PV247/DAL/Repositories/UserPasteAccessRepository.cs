using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Repositories
{
    public class UserPasteAccessRepository : EntityFrameworkRepository<UserPasteAccess, int>
    {
        public UserPasteAccessRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
