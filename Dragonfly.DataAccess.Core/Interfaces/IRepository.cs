﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Dragonfly.DataAccess.Core
{
    // ReSharper disable once InconsistentNaming
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        //void Update(Expression<Func<T>> where);
        void Update(IEnumerable<T> entities);
        void AddOrUpdate(T entity);
        void Remove(T entity);
        void Remove(Expression<Func<T, bool>> where);
        void Detach(T entity);
        void DetachAll();

        T Find<K>(K id);
        //T Find<K>(K id, params Expression<Func<T, object>>[] includes);
        T Find(Expression<Func<T, bool>> where);
        T Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);

        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> where);
        IEnumerable<T> Get(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
        IEnumerable<T> Get<TOrder>(Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order, int count = 0);
        IEnumerable<T> Get<TOrder>(int pageNumber, int pageSize, Expression<Func<T, TOrder>> order, SortOrder sortDirection);
        IEnumerable<T> Get<TOrder>(int pageNumber, int pageSize, Expression<Func<T, TOrder>> order, Expression<Func<T, bool>> where);
        IEnumerable<T> Get<TOrder>(int pageNumber, int pageSize, Expression<Func<T, TOrder>> order, SortOrder sortDirection, Expression<Func<T, bool>> where);

        IQueryable<T> Entity();
        IQueryable<T> Set();

        long Count();
        long Count(Expression<Func<T, bool>> where);

        void SaveChanges();
    }
}
