using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
    public class BadgeRepository : EntityFrameworkRepository<Badge, int>
    {
        public BadgeRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
