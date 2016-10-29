using System.Collections.Generic;
using ExpenseManager.Contract;
using AutoMapper;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Bussines.Infrastructure
{
    /// <summary>
    /// A base class for Query-enabled service, taken from unreleased project of RigantiInfrastructure solution, all credit goes to Tomas Herceg.
    /// </summary>
    /// <typeparam name="TListDTO">The type of the DTO used in the list of records, e.g. in the GridView control.</typeparam>
    public abstract class ExpenseManagerQueryAndCrudServiceBase<TEntity, TKey, TListDTO, TDTO> : ExpenseManagerCrudServiceBase<TEntity, TKey, TDTO> 
        where TEntity : IEntity<TKey>, new() 
        where TDTO : ExpenseManagerDTO<TKey>, new()
    {
        /// <summary>
        /// Gets the query object used to populate the list or records.
        /// </summary>
        public IQuery<TListDTO> Query { get; }

        protected ExpenseManagerQueryAndCrudServiceBase(IQuery<TListDTO> query, IRepository<TEntity, TDTO, TKey> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(repository, expenseManagerMapper, unitOfWorkProvider)
        {
            this.Query = query;
        }

        /// <summary>
        /// Gets the list of the DTOs using the Query object.
        /// </summary>
        public virtual IEnumerable<TListDTO> GetList()
        {
            using (UnitOfWorkProvider.Create())
            {
                return Query.Execute();
            }
        }
    }
}