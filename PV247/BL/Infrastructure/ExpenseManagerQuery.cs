using DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Infrastructure
{
    public abstract class ExpenseManagerQuery<T> : EntityFrameworkQuery<T>
    {
        public new ExpenseDbContext Context => (ExpenseDbContext)base.Context;

        protected ExpenseManagerQuery(IUnitOfWorkProvider provider): base(provider)
        {

        }
    }
}
