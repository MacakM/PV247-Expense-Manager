using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
    public class PlanRepository : EntityFrameworkRepository<Plan, int>
    {
        public PlanRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
