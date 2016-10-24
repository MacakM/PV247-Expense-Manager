using System.Collections.Generic;
using AutoMapper;
using BL.Infrastructure.DTOs;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Infrastructure.Services
{
    /// <summary>
    /// A base class for Query-enabled service, taken from unreleased project of RigantiInfrastructure solution, all credit goes to Tomas Herceg.
    /// </summary>
    /// <typeparam name="TListDTO">The type of the DTO used in the list of records, e.g. in the GridView control.</typeparam>
    public abstract class ExpenseManagerQueryAndCrudServiceBase<TEntity, TKey, TListDTO, TDetailDTO> : ExpenseManagerCrudServiceBase<TEntity, TKey, TDetailDTO> where TEntity : IEntity<TKey> where TDetailDTO : ExpenseManagerDTO<TKey>
    {
        /// <summary>
        /// Gets the query object used to populate the list or records.
        /// </summary>
        public IQuery<TListDTO> Query { get; private set; }

        protected ExpenseManagerQueryAndCrudServiceBase(IQuery<TListDTO> query, IRepository<TEntity, TKey> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider) : base(repository, expenseManagerMapper, unitOfWorkProvider)
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