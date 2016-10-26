using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL
{
    public abstract class ExpenseManagerQuery<T> : EntityFrameworkQuery<T>
    {
        internal new ExpenseDbContext Context => (ExpenseDbContext)base.Context;

        protected ExpenseManagerQuery(IUnitOfWorkProvider provider): base(provider)
        {

        }
    }
}
