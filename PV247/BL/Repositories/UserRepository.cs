using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
    // Will probably be removed if Identity is used
    public class UserRepository : EntityFrameworkRepository<User, int>
    {
        public UserRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
