using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Repositories
{
    public class BadgeRepository : EntityFrameworkRepository<Badge, int>
    {
        public BadgeRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
