using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
    public class UserBadgeRepository : EntityFrameworkRepository<UserBadge, int>
    {
        public UserBadgeRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
