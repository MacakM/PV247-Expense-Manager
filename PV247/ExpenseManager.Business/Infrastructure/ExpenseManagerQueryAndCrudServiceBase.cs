using System.Collections.Generic;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Infrastructure
{
    /// <summary>
    /// A base class for Query-enabled service, taken from unreleased project of RigantiInfrastructure solution, all credit goes to Tomas Herceg.
    /// </summary>
    /// <typeparam name="TList">The type of the  used in the list of records, e.g. in the GridView control.</typeparam>
    public abstract class ExpenseManagerQueryAndCrudServiceBase<TEntity, TKey, TList, T> : ExpenseManagerCrudServiceBase<TEntity, TKey, T> 
        where TEntity : class, IEntity<TKey>, new() 
        where T : ExpenseManager<TKey>, new()
    {
        /// <summary>
        /// Gets the query object used to populate the list or records.
        /// </summary>
        public IQuery<TList> Query { get; }

        protected ExpenseManagerQueryAndCrudServiceBase(IQuery<TList> query, ExpenseManagerRepository<TEntity, TKey> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(repository, expenseManagerMapper, unitOfWorkProvider)
        {
            this.Query = query;
        }

        /// <summary>
        /// Gets the list of the s using the Query object.
        /// </summary>
        public virtual IEnumerable<TList> GetList()
        {
            using (UnitOfWorkProvider.Create())
            {
                return Query.Execute();
            }
        }
    }
}