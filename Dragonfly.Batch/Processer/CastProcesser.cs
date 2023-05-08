using System;
using System.Collections.Generic;
using System.Linq;

namespace Dragonfly.Batch.Processer
{
    public class CastProcesser<TIn, TOut> : IProcesser<TIn, TOut> where TIn : class where TOut : class
    {
        public TOut Process(TIn item) 
        {
            if(!typeof(TOut).IsAssignableFrom(typeof(TIn)))
                throw new InvalidCastException($"{typeof(TOut).Name} is not assignable from {typeof(TIn)}");

            return item as TOut;
        }

        public IEnumerable<TOut> Process(IEnumerable<TIn> items)
        {
            return items.Select(item => item as TOut);
        }
    }
}
