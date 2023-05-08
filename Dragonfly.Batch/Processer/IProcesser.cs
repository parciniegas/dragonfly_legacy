using System.Collections.Generic;

namespace Dragonfly.Batch.Processer
{
    public interface IProcesser<in TInput, out TOutput>
    {
        TOutput Process(TInput item);
        IEnumerable<TOutput> Process(IEnumerable<TInput> items);
    }
}