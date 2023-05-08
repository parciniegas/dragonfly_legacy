using System.Collections.Generic;

namespace Dragonfly.Core.Writers
{
    public interface IWriter<in T>
    {
        void Write(T item);
        void Write(IEnumerable<T> items);
    }
}
