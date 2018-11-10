﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EFCore.Toolkit.Contracts;
using EFCore.Toolkit.Extensions;
using EFCore.Toolkit.Utils;
using Microsoft.EntityFrameworkCore;
#if !NET40
using System.Threading.Tasks;
#endif

namespace EFCore.Toolkit
{
    /// <summary>
    ///     Implementation of a generic repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly DbSet<T> DbSet;
        private readonly IDbContext context;
        private bool isDisposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericRepository{T}" /> class.
        /// </summary>
        // ReSharper disable once PublicConstructorInAbstractClass
        public GenericRepository(IDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.context = context;

            this.DbSet = this.context.Set<T>();
        }

        /// <inheritdoc />
        public IContext Context
        {
            get
            {
                return this.context;
            }
        }

        /// <inheritdoc />
        public IQueryable<T> Get()
        {
            return this.DbSet;
        }

        /// <inheritdoc />
        public virtual IEnumerable<T> GetAll()
        {
            return this.DbSet.AsEnumerable();
        }

        /// <inheritdoc />
        public T FindById(params object[] ids)
        {
            return this.DbSet.Find(ids);
        }

        /// <inheritdoc />
        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = this.DbSet.Where(predicate).AsEnumerable();
            return query;
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.Any(predicate);
        }

        /// <inheritdoc />
        public virtual T Add(T entity)
        {
            return this.DbSet.Add(entity).Entity;
        }

        /// <inheritdoc />
        public virtual TDerived Add<TDerived>(TDerived entity) where TDerived : class, T
        {
            return this.context.Set<TDerived>().Add(entity).Entity;
        }

        /// <inheritdoc />
        public virtual IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            this.DbSet.AddRange(entities);
            return entities;
        }

        /// <inheritdoc />
        public virtual T AddOrUpdate(T entity)
        {
            return ((DbContext)this.context).AddOrUpdate(entity);
        }

        /// <inheritdoc />
        public virtual T Update(T entity)
        {
            return this.context.Edit(entity);
        }

        /// <inheritdoc />
        public virtual T Update(T entity, T updateEntity)
        {
            return this.Update<T>(entity, updateEntity);
        }

        /// <inheritdoc />
        public virtual TDerived Update<TDerived>(TDerived entity, TDerived updateEntity) where TDerived : class, T
        {
            return this.context.Edit(entity, updateEntity);
        }

        /// <inheritdoc />
        public virtual T UpdateProperties<TValue>(T entity, params Expression<Func<T, TValue>>[] propertyExpressions)
        {
            this.context.UndoChanges(entity);

            var propertyNames = propertyExpressions.Select(pe => pe.GetPropertyInfo().Name).ToArray();
            this.context.ModifyProperties(entity, propertyNames);

            return entity;
        }

        /// <inheritdoc />
        public virtual T UpdateProperty<TValue>(T entity, Expression<Func<T, TValue>> propertyExpression, TValue value)
        {
            entity = this.UpdateProperties(entity, propertyExpression);

            entity.SetPropertyValue(propertyExpression.GetPropertyInfo().Name, value);
            return entity;
        }


        /// <inheritdoc />
        public virtual T Remove(T entity)
        {
            return this.context.Delete(entity);
        }

        /// <inheritdoc />
        public virtual void LoadReferenced<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty)
            where TEntity : class
            where TProperty : class
        {
            this.context.LoadReferenced(entity, navigationProperty);
        }

        /// <inheritdoc />
        public virtual ChangeSet Save()
        {
            return this.context.SaveChanges();
        }

#if !NET40
        /// <inheritdoc />
        public Task<ChangeSet> SaveAsync()
        {
            return this.context.SaveChangesAsync();
        }
#endif

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
                this.context.Dispose();
            }

            this.isDisposed = true;
        }
    }
}