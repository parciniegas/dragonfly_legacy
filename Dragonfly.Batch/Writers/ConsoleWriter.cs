using System;
using System.Collections.Generic;
using Dragonfly.Core;
using Dragonfly.Core.Writers;

namespace Dragonfly.Batch.Writers
{
    public class ConsoleWriter<T> : IWriter<T>
    {
        public void Write(T item)
        {
            //Console.WriteLine(JsonConvert.SerializeObject(item));
            item.GetProperties().ForEach(p =>
            {
                Console.Write($"{p.Name}: {item.GetPropertyValue(p.Name).ToString()}|");
            });
            Console.WriteLine();
        }

        public void Write(IEnumerable<T> items)
        {
            items.ForEach(Write);
        }
    }
}
