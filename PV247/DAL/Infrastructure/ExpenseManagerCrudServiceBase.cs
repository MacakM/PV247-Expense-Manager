using System;
using System.Linq.Expressions;
using APILayer;
using AutoMapper;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Infrastructure
{
    /// <summary>
    /// A base class for CRUD-enabled service, taken from unreleased project of RigantiInfrastructure solution, all credit goes to Tomas Herceg.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the entity primary key.</typeparam>
    /// <typeparam name="TDetailDTO">The type of the DTO used in the detail form.</typeparam>
    public abstract class ExpenseManagerCrudServiceBase<TEntity, TKey, TDetailDTO> where TEntity : IEntity<TKey> where TDetailDTO : ExpenseManagerDTO<TKey>
    {
        public IUnitOfWorkProvider UnitOfWorkProvider { get; }

        /// <summary>
        /// Gets the repository used to perform database operations with the entity.
        /// </summary>
        public IRepository<TEntity, TKey> Repository { get; }

        /// <summary>
        /// Gets the service that can map entities to DTOs and populate entities with changes made on DTOs.
        /// </summary>
        public IRuntimeMapper ExpenseManagerMapper { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="CrudFacadeBase{TEntity, TKey, TListDTO, TDetailDTO}"/> class.
        /// </summary>
        protected ExpenseManagerCrudServiceBase(IRepository<TEntity, TKey> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider)
        {
            this.UnitOfWorkProvider = unitOfWorkProvider;
            this.Repository = repository;
            this.ExpenseManagerMapper = expenseManagerMapper.DefaultContext.Mapper;
        }

        /// <summary>
        /// Gets the detail DTO for an entity with the specified ID.
        /// </summary>
        public virtual TDetailDTO GetDetail(TKey id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var entity = Repository.GetById(id, EntityIncludes);
                return ExpenseManagerMapper.Map<TEntity, TDetailDTO>(entity);
            }
        }

        /// <summary>
        /// Gets a new detail DTO with default values.
        /// </summary>
        public virtual TDetailDTO InitializeNew()
        {
            using (UnitOfWorkProvider.Create())
            {
                var entity = Repository.InitializeNew();
                return ExpenseManagerMapper.Map<TEntity, TDetailDTO>(entity);
            }
        }

        /// <summary>
        /// Saves the changes on the specified DTO to the database.
        /// </summary>
        public virtual void Save(TDetailDTO detail)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                TEntity entity;
                var isNew = false;
                if (detail.Id.Equals(default(TKey)))
                {
                    // the record is new
                    entity = Repository.InitializeNew();
                    isNew = true;
                }
                else
                {
                    entity = Repository.GetById(detail.Id, EntityIncludes);
                }

                // populate the entity
                PopulateDetailToEntity(detail, entity);

                // save
                Save(entity, isNew, detail, uow);
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
        /// Transfers the changes on DTO made by the user to the corresponding database entity.
        /// </summary>
        protected virtual void PopulateDetailToEntity(TDetailDTO detail, TEntity entity)
        {
            ExpenseManagerMapper.Map(detail, entity);
        }

        /// <summary>
        /// Saves the changes made to the entity in the database, and if the entity was inserted, updates the DTO with its ID.
        /// </summary>
        protected virtual void Save(TEntity entity, bool isNew, TDetailDTO detail, IUnitOfWork uow)
        {
            // insert or update
            if (isNew)
            {
                Repository.Insert(entity);
            }
            else
            {
                Repository.Update(entity);
            }

            // save
            uow.Commit();
            detail.Id = entity.Id;
        }

        /// <summary>
        /// Gets a list of navigation property expressions that should be included when the service loads the entity.
        /// </summary>
        protected virtual Expression<Func<TEntity, object>>[] EntityIncludes => new Expression<Func<TEntity, object>>[] { };


    }
}