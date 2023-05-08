using System.Collections.Generic;

namespace Dragonfly.Core.Readers
{
    public interface IReader<out T>
    {
        IEnumerable<T> Read();
        IEnumerable<T> Read(int skip);
        IEnumerable<T> Read(int skip, int batch);
    }
}
