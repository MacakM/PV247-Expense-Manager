using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ExpenseManager.Database.Infrastructure.UnitOfWork;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.Infrastructure.Repository
{
    /// <summary>
    /// A base implementation of a repository.
    /// </summary>
    public class ExpenseManagerRepository<TEntity, TKey> 
        where TEntity : class, IEntity<TKey>, new() 
    {
        private readonly IUnitOfWorkProvider _provider;

        /// <summary>
        /// Gets the <see cref="DbContext"/>.
        /// </summary>
        protected DbContext Context => ExpenseManagerUnitOfWork.TryGetDbContext(_provider);

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseManagerRepository{TEntity, TKey}"/> class.
        /// </summary>
        public ExpenseManagerRepository(IUnitOfWorkProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Get entity with specified ID.
        /// </summary>
        /// <param name="id">id of object</param>
        /// <param name="includes">includes</param>
        /// <returns></returns>
        public TEntity GetById(TKey id, params string[] includes)
        {
            return GetByIds(new[] { id }, includes).FirstOrDefault();
        }

        /// <summary>
        /// Gets a list of entities with specified IDs.
        /// </summary>
        /// <remarks>
        /// This method is not suitable for large amounts of entities - the reasonable limit of number of IDs is 30.
        /// </remarks>
        public IList<TEntity> GetByIds(IEnumerable<TKey> ids, params string[] includes)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return query.Where(i => ids.Contains(i.Id)).ToList();
        }

        /// <summary>
        /// Inserts the specified entity into the table according to given entity.
        /// </summary>
        public void Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Inserts the specified entities into the table according to given entities.
        /// </summary>
        public void Insert(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities.ToList())
            {
                Insert(entity);
            }
        }

        /// <summary>
        /// Updates entity.
        /// </summary>
        /// <param name="entity">used entity</param>
        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Updates entities.
        /// </summary>
        /// <param name="entities">entities</param>
        public void Update(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities.ToList())
            {
                Update(entity);
            }
        }

        /// <summary>
        /// Saves the changes on the specified entity to the database.
        /// </summary>
        public void InsertOrUpdate(TEntity entity)
        {
            var isNew = entity.Id.Equals(default(TKey));
            if (isNew)
            {
                Insert(entity);
            }
            else
            {
                Update(entity);
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        public void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        /// <summary>
        /// Deletes the specified entities according to given entities.
        /// </summary>
        public void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities.ToList())
            {
                Delete(entity);
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        public virtual void Delete(TKey id)
        {
            var entity = GetByIds(new[] { id }).FirstOrDefault();
            Context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        public void Delete(IEnumerable<TKey> ids)
        {
            foreach (var id in ids)
            {
                Delete(id);
            }
        }
    }
}
