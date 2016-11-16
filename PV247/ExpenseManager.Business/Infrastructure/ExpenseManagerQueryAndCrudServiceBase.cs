using System.Collections.Generic;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Infrastructure.Query;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Infrastructure
{
    /// <summary>
    /// A base class for Query-enabled service, taken from unreleased project of RigantiInfrastructure solution, all credit goes to Tomas Herceg.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T"></typeparam>
    public abstract class ExpenseManagerQueryAndCrudServiceBase<TEntity, TKey, T> : ExpenseManagerCrudServiceBase<TEntity, TKey, T> 
        where TEntity : class, IEntity<TKey>, new() 
        where T : BusinessObject<TKey>, new() 
    {
        /// <summary>
        /// Gets the query object used to populate the list or records.
        /// </summary>
        public ExpenseManagerQuery<TEntity> Query { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="repository"></param>
        /// <param name="expenseManagerMapper"></param>
        /// <param name="unitOfWorkProvider"></param>
        protected ExpenseManagerQueryAndCrudServiceBase(ExpenseManagerQuery<TEntity> query, ExpenseManagerRepository<TEntity, TKey> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(repository, expenseManagerMapper, unitOfWorkProvider)
        {
            this.Query = query;
        }

        /// <summary>
        /// Gets the list of the s using the Query object.
        /// </summary>
        public virtual IList<T> GetList()
        {
            using (UnitOfWorkProvider.Create())
            {
                return ExpenseManagerMapper.Map<IList<TEntity>, IList<T>>(Query.Execute());
            }
        }
    }
}