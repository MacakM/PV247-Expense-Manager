using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using APILayer;
using AutoMapper;
using DAL.Infrastructure.UnitOfWork;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Infrastructure.Repository
{
    /// <summary>
    /// A base implementation of a repository.
    /// </summary>
    public class ExpenseManagerRepository<TEntity, TDTO, TKey> : IRepository<TEntity, TDTO, TKey> 
        where TEntity : class, IEntity<TKey>, new() 
        where TDTO : ExpenseManagerDTO<TKey>, new()
    {
        private const char ExpressionSeparator = '.';

        private readonly IUnitOfWorkProvider _provider;

        /// <summary>
        /// Gets the <see cref="DbContext"/>.
        /// </summary>
        protected DbContext Context => ExpenseManagerUnitOfWork.TryGetDbContext(_provider);

        /// <summary>
        /// Expense manager Mapper
        /// </summary>
        protected IRuntimeMapper ExpenseManagerMapper { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseManagerRepository{TEntity, TDTO, TKey}"/> class.
        /// </summary>
        public ExpenseManagerRepository(IUnitOfWorkProvider provider, Mapper expenseManagerMapper)
        {
            this._provider = provider;
            ExpenseManagerMapper = expenseManagerMapper.DefaultContext.Mapper;
        }

        /// <summary>
        /// Get DTO with specified ID.
        /// </summary>
        /// <param name="id">id of object</param>
        /// <param name="includes">includes</param>
        /// <returns></returns>
        public TDTO GetById(TKey id, params Expression<Func<TDTO, object>>[] includes)
        {
            return GetByIds(new[] { id }, includes).FirstOrDefault();
        }

        /// <summary>
        /// Gets a list of dtos with specified IDs.
        /// </summary>
        /// <remarks>
        /// This method is not suitable for large amounts of entities - the reasonable limit of number of IDs is 30.
        /// </remarks>
        public IList<TDTO> GetByIds(IEnumerable<TKey> ids, params Expression<Func<TDTO, object>>[] includes)
        {
            return GetEntitiesByIds(ids, includes)
                .Select(entity => ExpenseManagerMapper.Map<TEntity, TDTO>(entity))
                .ToList();
        }

        private IList<TEntity> GetEntitiesByIds(IEnumerable<TKey> ids, params Expression<Func<TDTO, object>>[] includes)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            var includeList = ProcessIncludesList(includes);
            query = includeList.Aggregate(query, (current, include) => current.Include(include));
            return query.Where(i => ids.Contains(i.Id)).ToList();
        }

        /// <summary>
        /// Process list of includes.
        /// </summary>
        /// <param name="includes">includes</param>
        protected static IEnumerable<string> ProcessIncludesList(Expression<Func<TDTO, object>>[] includes)
        {
            var includeList = new List<string>();
            foreach (var expressionBodyData in includes
                .Select(include => include.Body.ToString())
                .Where(expressionBodyString => expressionBodyString.Contains(ExpressionSeparator))
                .Select(expressionBodyString => expressionBodyString.Split(ExpressionSeparator)))
            {
                if (expressionBodyData.Length != 2)
                {
                    Debug.WriteLine("GetByIds(...) - includes do not currently support multiple nesting");
                    continue;
                }
                includeList.Add(expressionBodyData[1]);
            }
            return CheckIncludes(includeList);
        }

        private static IEnumerable<string> CheckIncludes(List<string> includeList)
        {
            var entityPropertyNames = typeof (TEntity).GetProperties().Select(propInfo => propInfo.Name);
            var checkedIncludes = includeList.Where(include => entityPropertyNames.Contains(include)).ToList();
            var badIncludes = includeList.Except(checkedIncludes).ToList();
            foreach (var badInclude in badIncludes)
            {
                Debug.WriteLine($"WARNING: Property named {badInclude} does not exists.");
            }
            return includeList;
        }

        /// <summary>
        /// Inserts the specified entity into the table according to given dto.
        /// </summary>
        public void Insert(TDTO dto)
        {
            var entity = ExpenseManagerMapper.Map<TDTO, TEntity>(dto);
            Context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Inserts the specified entities into the table according to given dtos.
        /// </summary>
        public void Insert(IEnumerable<TDTO> dtos)
        {
            foreach (var dto in dtos.ToList())
            {
                Insert(dto);
            }
        }

        /// <summary>
        /// Updates entity.
        /// </summary>
        /// <param name="dto">used DTO</param>
        /// <param name="entityIncludes">includes</param>
        public void Update(TDTO dto, params Expression<Func<TDTO, object>>[] entityIncludes)
        {
            var entity = GetEntitiesByIds(new[] {dto.Id}, entityIncludes).FirstOrDefault();
            ExpenseManagerMapper.Map(dto, entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Updates entities.
        /// </summary>
        /// <param name="dtos">DTOs</param>
        /// <param name="entityIncludes">includes</param>
        public void Update(IEnumerable<TDTO> dtos, params Expression<Func<TDTO, object>>[] entityIncludes)
        {
            foreach (var dto in dtos.ToList())
            {
                Update(dto);
            }
        }

        /// <summary>
        /// Saves the changes on the specified DTO to the database.
        /// </summary>
        public void InsertOrUpdate(TDTO dto, params Expression<Func<TDTO, object>>[] entityIncludes)
        {
            var isNew = dto.Id.Equals(default(TKey));
            if (isNew)
            {
                Insert(dto);
            }
            else
            {
                Update(dto, entityIncludes);
            }
        }

        /// <summary>
        /// Deletes the specified dto.
        /// </summary>
        public void Delete(TDTO dto)
        {
            Delete(dto.Id);
        }

        /// <summary>
        /// Deletes the specified entities according to given dtos.
        /// </summary>
        public void Delete(IEnumerable<TDTO> dtos)
        {
            foreach (var dto in dtos.ToList())
            {
                Delete(dto);
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        public virtual void Delete(TKey id)
        {
            var entity = GetEntitiesByIds(new[] { id }).FirstOrDefault();
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
