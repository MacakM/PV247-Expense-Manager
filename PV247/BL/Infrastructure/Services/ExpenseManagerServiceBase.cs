using Riganti.Utils.Infrastructure.Core;

namespace BL.Infrastructure.Services
{
    public class ExpenseManagerServiceBase
    {
        public IUnitOfWorkProvider UnitOfWorkProvider { get; set; }
    }
}
