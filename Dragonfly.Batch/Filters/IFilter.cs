using System.Collections.Generic;

namespace Dragonfly.Batch.Filters
{
    public interface IFilter<T>
    {
        T Filter(T input);
        IEnumerable<T> Filter(IEnumerable<T> input);
    }
}
