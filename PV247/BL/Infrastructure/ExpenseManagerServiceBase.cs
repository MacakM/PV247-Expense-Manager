using Riganti.Utils.Infrastructure.Core;

namespace BL.Infrastructure
{
    public class ExpenseManagerServiceBase
    {
        public IUnitOfWorkProvider UnitOfWorkProvider { get; set; }
    }
}
