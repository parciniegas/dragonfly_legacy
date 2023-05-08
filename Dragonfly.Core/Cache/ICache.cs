using System;

namespace Dragonfly.Core
{
    public interface ICache
    {
        T Get<T>(string key) where T : class;
        void Add<T>(string key, T value) where T : class;
        void Add<T>(string key, T value, TimeSpan timeSpan) where T : class;
        bool Contains(string key);
    }
}
