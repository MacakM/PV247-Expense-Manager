using System;
using System.Linq.Expressions;
using APILayer;
using AutoMapper;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Infrastructure
{
    /// <summary>
    /// A base class for CRUD-enabled service, taken from unreleased project of RigantiInfrastructure solution, all credit goes to Tomas Herceg.
    /// </summary>
    /// <typeparam name="TKey">The type of the entity primary key.</typeparam>
    /// <typeparam name="TDTO">The type of the DTO used in the detail form.</typeparam>
    public abstract class ExpenseManagerCrudServiceBase<TEntity, TKey, TDTO> where TEntity : IEntity<TKey>, new() where TDTO : ExpenseManagerDTO<TKey>, new()
    {
        public IUnitOfWorkProvider UnitOfWorkProvider { get; }

        /// <summary>
        /// Gets the repository used to perform database operations with the entity.
        /// </summary>
        public IRepository<TEntity, TDTO, TKey> Repository { get; }

        /// <summary>
        /// Gets the service that can map entities to DTOs and populate entities with changes made on DTOs.
        /// </summary>
        public IRuntimeMapper ExpenseManagerMapper { get; }


        protected ExpenseManagerCrudServiceBase(IRepository<TEntity, TDTO, TKey> repository, Mapper expenseManagerMapper, IUnitOfWorkProvider unitOfWorkProvider)
        {
            this.UnitOfWorkProvider = unitOfWorkProvider;
            this.Repository = repository;
            this.ExpenseManagerMapper = expenseManagerMapper.DefaultContext.Mapper;
        }

        /// <summary>
        /// Gets the detail DTO for an entity with the specified ID.
        /// </summary>
        public virtual TDTO GetDetail(TKey id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return Repository.GetById(id, EntityIncludes);
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
        /// Saves the changes on the specified DTO to the database.
        /// </summary>
        public virtual void Save(TDTO dto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                Repository.InsertOrUpdate(dto, EntityIncludes);
            }
        }

        /// <summary>
        /// Gets a list of navigation property expressions that should be included when the service loads the entity.
        /// </summary>
        protected virtual Expression<Func<TDTO, object>>[] EntityIncludes => new Expression<Func<TDTO, object>>[] { };
    }
}