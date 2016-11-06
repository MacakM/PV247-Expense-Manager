using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Business.Infrastructure
{
    
    /// <summary>
    /// A base class for CRUD-enabled service, taken from unreleased project of RigantiInfrastructure solution, all credit goes to Tomas Herceg.
    /// </summary>
    /// <typeparam name="TKey">The type of the entity primary key.</typeparam>
    /// <typeparam name="T">The type of the  used in the detail form.</typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class ExpenseManagerCrudServiceBase<TEntity, TKey, T> 
        where TEntity : class, IEntity<TKey>, new() 
        where T : BusinessObject<TKey>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        public IUnitOfWorkProvider UnitOfWorkProvider { get; }

        /// <summary>
        /// Gets the repository used to perform database operations with the entity.
        /// </summary>
        public ExpenseManagerRepository<TEntity, TKey> Repository { get; }

        /// <summary>
        /// Gets the service that can map entities to s and populate entities with changes made on s.
        /// </summary>
        public IRuntimeMapper ExpenseManagerMapper { get; }

        /// <summary>
        /// Ctor for ExpenseManagerCrudServiceBase
        /// </summary>
        /// <param name="repository">repository used by this service</param>
        /// <param name="expenseManagerMapper">mapper</param>
        /// <param name="unitOfWorkProvider">uow provider</param>
        protected ExpenseManagerCrudServiceBase(ExpenseManagerRepository<TEntity, TKey> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider)
        {
            UnitOfWorkProvider = unitOfWorkProvider;
            Repository = repository;
            ExpenseManagerMapper = expenseManagerMapper.DefaultContext.Mapper;
        }

        /// <summary>
        /// Gets the detail  for an entity with the specified ID.
        /// </summary>
        public virtual T GetDetail(TKey id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var entity = Repository.GetById(id, EntityIncludes);
                return ExpenseManagerMapper.Map<TEntity, T>(entity);
            }
        }

        /// <summary>
        /// Deletes the entity with the specified ID.
        /// </summary>
        public virtual void Delete(TKey id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                Repository.Delete(id);
                uow.Commit();
            }
        }
        
        /// <summary>
        /// Saves the changes on the specified  to the database.
        /// </summary>
        public virtual void Save(T item)
        {
            var entity = ExpenseManagerMapper.Map<T, TEntity>(item);
            using (UnitOfWorkProvider.Create())
            {
                var isNew = item.Id.Equals(default(TKey));
                if (isNew)
                {
                    Repository.Insert(entity);
                }
                else
                {
                    Repository.Update(entity);
                }
            }
        }

        /// <summary>
        /// Gets a list of navigation property expressions that should be included when the service loads the entity.
        /// </summary>
        protected abstract string[] EntityIncludes { get; }
    }
}