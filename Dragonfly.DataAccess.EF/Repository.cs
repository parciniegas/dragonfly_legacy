using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Dragonfly.Core;
using Dragonfly.DataAccess.Core;
using Dragonfly.DataAccess.EF.Base;

namespace Dragonfly.DataAccess.EF
{
    // ReSharper disable once InconsistentNaming
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Private Fields
        private readonly IDbSet<T> _entitySet;
        private readonly BaseContext _context;
        #endregion Private Fields

        #region Constructors
        public Repository(BaseContext context)
        {
            _context = context;
            _entitySet = context.Set<T>();
        }
        #endregion

        #region Public Persistence Methods
        public void Add(T entity)
        {
            //var properties = entity.GetProperties();
            //properties.ForEach(p =>
            //{
            //    Attribute.IsDefined(typeof(KeyNotFoundException))
            //});
            _context.Entry(entity).State = EntityState.Added;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        //public void Update(Expression<Func<T>> @where)
        //{
        //    //IEnumerable<T> entities ;
        //}

        public void Update(IEnumerable<T> entities)
        {
            entities.ForEach(Add);
        }

        public void AddOrUpdate(T entity)
        {
            _entitySet.AddOrUpdate(entity);
        }

        public void Remove(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public void Remove(Expression<Func<T, bool>> where)
        {
            var entities = _entitySet.Where(where).AsEnumerable();
            foreach (var entity in entities)
                _entitySet.Remove(entity);
        }

        public void Detach(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public void DetachAll()
        {
            _context.ChangeTracker.Entries<T>().ForEach(e => e.State = EntityState.Detached);
        }
        #endregion

        #region Public Read Methods
        public T Find<K>(K id)
        {
            return _entitySet.Find(id);
        }

        //public T Find<K>(K id, params Expression<Func<T, object>>[] includes)
        //{
        //    var query = _entitySet.Find(id);
        //    return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        //}

        public T Find(Expression<Func<T, bool>> where)
        {
            return _entitySet.Find(where);
        }

        public T Find(Expression<Func<T, bool>> @where, params Expression<Func<T, object>>[] includes)
        {
            var query = _entitySet.AsNoTracking().Where(where);
            return includes
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty))
                .FirstOrDefault();
        }

        public IEnumerable<T> Get()
        {
            return _entitySet.AsNoTracking();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> where)
        {
            return _entitySet.Where(where).AsNoTracking();
        }

        public IEnumerable<T> Get(params Expression<Func<T, object>>[] includes)
        {
            var query = _entitySet.AsNoTracking();

            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            var query = _entitySet.Where(where).AsNoTracking();
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public IEnumerable<T> Get<TSort>(Expression<Func<T, bool>> where, Expression<Func<T, TSort>> order, int count = 0)
        {
            return count == 0 ?
                       _entitySet.AsNoTracking().Where(where).OrderBy(order) :
                       _entitySet.AsNoTracking().Where(where).OrderBy(order).Take(count);
        }

        public IEnumerable<T> Get<TSort>(int pageNumber, int pageSize, Expression<Func<T, TSort>> order, SortOrder sortDirection = SortOrder.Ascending)
        {
            var startRecord = (pageNumber - 1) * pageSize;
            IEnumerable<T> page = null;
            switch (sortDirection)
            {
                case SortOrder.Ascending:
                    page = _entitySet.AsNoTracking().OrderBy(order).Skip(startRecord).Take(pageSize);
                    break;
                case SortOrder.Descending:
                    page = _entitySet.AsNoTracking().OrderByDescending(order).Skip(startRecord).Take(pageSize);
                    break;
                case SortOrder.Unspecified:
                    break;
                default:
                    page = _entitySet.AsNoTracking().OrderBy(order).Skip(startRecord).Take(pageSize);
                    break;
            }

            return page;
        }

        public IEnumerable<T> Get<TSort>(int pageNumber, int pageSize, Expression<Func<T, TSort>> order, Expression<Func<T, bool>> where)
        {
            var startRecord = (pageNumber - 1) * pageSize;
            var page = _entitySet.AsNoTracking().OrderBy(order).Where(where).Skip(startRecord).Take(pageSize);

            return page;
        }

        public IEnumerable<T> Get<TSort>(int pageNumber, int pageSize, Expression<Func<T, TSort>> order, SortOrder sortDirection, Expression<Func<T, bool>> where)
        {
            var startRecord = (pageNumber - 1) * pageSize;
            IEnumerable<T> page;
            switch (sortDirection)
            {
                case SortOrder.Ascending:
                    page = _entitySet.AsNoTracking().OrderBy(order).Where(where).Skip(startRecord).Take(pageSize);
                    break;
                case SortOrder.Descending:
                    page = _entitySet.AsNoTracking().OrderByDescending(order).Where(where).Skip(startRecord).Take(pageSize);
                    break;
                case SortOrder.Unspecified:
                    page = _entitySet.AsNoTracking().OrderBy(order).Where(where).Skip(startRecord).Take(pageSize);
                    break;
                default:
                    page = _entitySet.AsNoTracking().OrderBy(order).Where(where).Skip(startRecord).Take(pageSize);
                    break;
            }

            return page;
        }

        public IQueryable<T> Entity()
        {
            return _entitySet.AsNoTracking().AsQueryable();
        }

        public IQueryable<T> Set()
        {
            return _entitySet.AsNoTracking().AsQueryable();
        }

        public virtual long Count()
        {
            return _entitySet.AsNoTracking().Count();
        }

        public long Count(Expression<Func<T, bool>> where)
        {
            return _entitySet.AsNoTracking().Where(where).Count();
        }

        //public virtual long Count(string table)
        //{
        //    var sql = $"select rows from sys.partitions where object_id = OBJECT_ID('{table}')";
        //    var count = _context.Database.SqlQuery<long>(sql).FirstOrDefault();
        //    return count;
        //}

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        #endregion
    }
}
