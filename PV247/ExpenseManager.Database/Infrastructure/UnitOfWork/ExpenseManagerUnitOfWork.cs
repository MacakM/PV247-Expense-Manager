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
        private bool hasOwnContext;

        /// <summary>
        /// Gets the <see cref="DbContext"/>.
        /// </summary>
        internal ExpenseDbContext Context { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseManagerUnitOfWork"/> class.
        /// </summary>
        public ExpenseManagerUnitOfWork(IUnitOfWorkProvider provider, bool reuseParentContext)
        {
            if (reuseParentContext)
            {
                var parentUow = provider.GetCurrent() as ExpenseManagerUnitOfWork;
                if (parentUow != null)
                {
                    this.Context = parentUow.Context;
                    return;
                }
            }

            // internal DbContext shall not be injected in order to increase persistence separation
            this.Context = new ExpenseDbContext(((ExpenseManagerUnitOfWorkProvider)provider).ConnectionOptions.Value.ConnectionString);
            hasOwnContext = true;
        }


        /// <summary>
        /// Commits this instance when we have to.
        /// </summary>
        public override void Commit()
        {
            if (hasOwnContext)
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
            }

        }

        /// <summary>
        /// Disposes the context.
        /// </summary>
        protected override void DisposeCore()
        {
            if (hasOwnContext)
            {
                Context.Dispose();
            }
        }

        /// <summary>
        /// Tries to get the <see cref="DbContext"/> in the current scope.
        /// </summary>
        public static DbContext TryGetDbContext(IUnitOfWorkProvider provider)
        {
            var uow = provider.GetCurrent() as ExpenseManagerUnitOfWork;
            if (uow == null)
            {
                throw new InvalidOperationException("The EntityFrameworkRepository must be used in a unit of work of type EntityFrameworkUnitOfWork!");
            }
            return uow.Context;
        }
    }
}