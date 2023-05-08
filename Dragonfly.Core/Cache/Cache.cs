using System;
using System.Runtime.Caching;

namespace Dragonfly.Core
{
    public class Cache : ICache
    {
        #region Private Fields
        private readonly ObjectCache _lCache = MemoryCache.Default;
        private readonly object _locker = new object();
        #endregion

        #region ICache Implementation
        public T Get<T>(string key) where T : class
        {
            T value;
            lock (_locker)
            {
                value = (T)_lCache.Get(key);
            }
            return value;
        }

        public void Add<T>(string key, T value) where T : class
        {
            lock (_locker)
            {
                _lCache.Add(key, value, DateTimeOffset.Now.AddSeconds(60.0));
            }
        }

        public void Add<T>(string key, T value, TimeSpan timeSpan) where T : class
        {
            lock (_locker)
            {
                _lCache.Add(key, value, DateTimeOffset.Now.Add(timeSpan));
            }
        }

        public bool Contains(string key)
        {
            lock (_locker)
            {
                return _lCache.Contains(key);
            }
        }
        #endregion
    }
}
