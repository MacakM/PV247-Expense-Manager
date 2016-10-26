using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Repositories
{
    public class UserPasteAccessRepository : ExpenseManagerRepository<UserPasteAccess, int>
    {
        public UserPasteAccessRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
