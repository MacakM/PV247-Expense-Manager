using System;
using System.Data.Entity;
using System.Diagnostics;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.Infrastructure.UnitOfWork
{
    /// <summary>
    /// An implementation of unit of work in Entity ramework.
    /// </summary>
    public class ExpenseManagerUnitOfWork : UnitOfWorkBase
    {
        private readonly bool _hasOwnContext;

        /// <summary>
        /// Gets the <see cref="DbContext"/>.
        /// </summary>
        internal ExpenseDbContext Context { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseManagerUnitOfWork"/> class.
        /// </summary>
        public ExpenseManagerUnitOfWork(IUnitOfWorkProvider provider, bool reuseParentContext)
        {
            if (reuseParentContext)
            {
                var parentunitOfWork = provider.GetCurrent() as ExpenseManagerUnitOfWork;
                if (parentunitOfWork != null)
                {
                    this.Context = parentunitOfWork.Context;
                    return;
                }
            }

            var unitOfWorkProvider = (ExpenseManagerUnitOfWorkProvider) provider;
           
            Context = unitOfWorkProvider.ConnectionOptions == null
                ? unitOfWorkProvider.DbContextFactory?.Invoke() as ExpenseDbContext 
                // internal DbContext shall not be injected in some scenarios in order to increase persistence separation
                : new ExpenseDbContext(unitOfWorkProvider.ConnectionOptions.ConnectionString);

            _hasOwnContext = true;
        }


        /// <summary>
        /// Commits this instance when we have to.
        /// </summary>
        public override void Commit()
        {
            if (_hasOwnContext)
            {
                base.Commit();
            }
        }

        /// <summary>
        /// Commits the changes.
        /// </summary>
        protected override void CommitCore()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
               Debug.WriteLine("An exception was thrown while performing SaveChanges():" + ex.Message);
               throw;
            }
        }

        /// <summary>
        /// Disposes the context.
        /// </summary>
        protected override void DisposeCore()
        {
            if (_hasOwnContext)
            {
                Context.Dispose();
            }
        }

        /// <summary>
        /// Tries to get the <see cref="DbContext"/> in the current scope.
        /// </summary>
        public static DbContext TryGetDbContext(IUnitOfWorkProvider provider)
        {
            var unitOfWork = provider.GetCurrent() as ExpenseManagerUnitOfWork;
            if (unitOfWork == null)
            {
                throw new InvalidOperationException("The EntityFrameworkRepository must be used in a unit of work of type EntityFrameworkUnitOfWork!");
            }
            return unitOfWork.Context;
        }
    }
}