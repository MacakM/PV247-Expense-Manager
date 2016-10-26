using DAL.Enums;
using DAL.Infrastructure;
using Microsoft.Extensions.Options;
using Riganti.Utils.Infrastructure.Core;

namespace DAL
{
    public class ExpenseManagerUnitOfWorkProvider : EntityFrameworkUnitOfWorkProvider
    {
        internal IOptions<ConnectionOptions> ConnectionOptions { get; }

        public ExpenseManagerUnitOfWorkProvider(IOptions<ConnectionOptions> connectionOptions,
            IUnitOfWorkRegistry registry)
            : base(registry)
        {
            ConnectionOptions = connectionOptions;
        }

        protected override EntityFrameworkUnitOfWork CreateUnitOfWork(DbContextOptions options)
        {
            return new ExpenseManagerUnitOfWork(this, options);
        }
    }
}
