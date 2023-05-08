using System;
using System.Collections.Generic;
using System.Linq;
using Dragonfly.Core.Writers;

namespace Dragonfly.TestApps.TestBatch
{
    public class CustomWriterIn : IWriter<InputLog>
    {
        private long _counter;

        public void Write(InputLog item)
        {
            _counter++;
            Console.CursorLeft = 0;
            Console.Write($"Item: {_counter} => Id: {item.Id}".PadRight(50));
        }

        public void Write(IEnumerable<InputLog> items)
        {
            foreach (var outputLog in items)
            {
                Write(outputLog);
            }
        }
    }

    public class CustomWriterOut : IWriter<OutputLog>
    {
        private long _counter;

        public void Write(OutputLog item)
        {
            _counter++;
            Console.CursorLeft = 0;
            Console.Write($"Item: {_counter} => Id: {item.Id}".PadRight(50));
        }

        public void Write(IEnumerable<OutputLog> items)
        {
            _counter += items.Count();
            Console.WriteLine($"Writing {_counter} items to SqlServer. ");
        }
    }
}
