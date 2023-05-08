using System;
using System.Collections.Generic;
using System.Data.Entity;
using Dragonfly.DataAccess.Core;
using Dragonfly.DataAccess.EF.Base;

namespace Dragonfly.DataAccess.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Fields
        private readonly Dictionary<string, object> _repositories;
        private readonly BaseContext _context;
        private bool _disposed;
        #endregion Private Fields

        public UnitOfWork(BaseContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        // ReSharper disable once InconsistentNaming
        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T).Name;

            if (_repositories.ContainsKey(type))
                return (Repository<T>)_repositories[type];

            var repositoryType = typeof(Repository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
            _repositories.Add(type, repositoryInstance);

            return (Repository<T>)_repositories[type];
        }

        public void ExecuteSqlCommand(string command)
        {
            _context.Database.ExecuteSqlCommand(command);
        }

        public DbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void SetAutoDetectChanges(bool enabled)
        {
            _context.Configuration.AutoDetectChangesEnabled = enabled;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}

