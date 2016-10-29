using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ExpenseManager.Contract;

namespace ExpenseManager.Database.Infrastructure.Repository
{
    /// <summary>
    /// Interface for Repository.
    /// </summary>
    /// <typeparam name="TEntity">entity</typeparam>
    /// <typeparam name="TDTO">DTO</typeparam>
    /// <typeparam name="TKey">primary key</typeparam>
    public interface IRepository<TEntity, TDTO, in TKey> 
        where TDTO : ExpenseManagerDTO<TKey>, new()
    {

        /// <summary>
        /// Gets the entity with specified ID.
        /// </summary>
        TDTO GetById(TKey id, params Expression<Func<TDTO, object>>[] includes);

        /// <summary>
        /// Gets a list of entities with specified IDs.
        /// </summary>
        /// <remarks>This method is not suitable for large amounts of entities - the reasonable limit of number of IDs is 30.</remarks>
        IList<TDTO> GetByIds(IEnumerable<TKey> ids, params Expression<Func<TDTO, object>>[] includes);

        /// <summary>
        /// Inserts the specified entity into the table.
        /// </summary>
        void Insert(TDTO entity);

        /// <summary>
        /// Inserts the specified entities into the table.
        /// </summary>
        void Insert(IEnumerable<TDTO> entities);

        /// <summary>
        /// Marks the specified entity as updated.
        /// </summary>
        void Update(TDTO entity, params Expression<Func<TDTO, object>>[] entityIncludes);

        /// <summary>
        /// Marks the specified entities as updated.
        /// </summary>
        void Update(IEnumerable<TDTO> entities, params Expression<Func<TDTO, object>>[] entityIncludes);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        void Delete(TDTO entity);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        void Delete(IEnumerable<TDTO> entities);

        /// <summary>
        /// Deletes an entity with the specified ID.
        /// </summary>
        void Delete(TKey id);

        /// <summary>
        /// Deletes entities with the specified IDs.
        /// </summary>
        void Delete(IEnumerable<TKey> ids);

        /// <summary>
        /// Saves the changes on the specified DTO to the database.
        /// </summary>
        void InsertOrUpdate(TDTO dto, params Expression<Func<TDTO, object>>[] entityIncludes);
    }
}