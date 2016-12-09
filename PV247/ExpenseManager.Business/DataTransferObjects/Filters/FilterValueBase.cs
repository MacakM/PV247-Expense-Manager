using System;
using System.Linq;
using System.Linq.Expressions;
using ExpenseManager.Database.DataAccess.FilterInterfaces;

namespace ExpenseManager.Business.DataTransferObjects.Filters
{
    internal abstract class FilterValueBase<TEntity, TValue> : IFilter<TEntity>, IFilterValue<TValue>
    {
        public TValue Value { set; private get; }

        public abstract Expression<Func<TEntity, bool>> GetWhereCondition(TValue value);

        public IQueryable<TEntity> FilterQuery(IQueryable<TEntity> queryable)
            => queryable.Where(GetWhereCondition(Value));
    }
}