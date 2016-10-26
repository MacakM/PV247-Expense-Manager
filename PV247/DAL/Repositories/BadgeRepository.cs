using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Repositories
{
    public class BadgeRepository : ExpenseManagerRepository<Badge, int>
    {
        public BadgeRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
