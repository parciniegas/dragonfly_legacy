using System.Collections.Generic;
using System.Linq;
using Dragonfly.Batch.Processer;

namespace Dragonfly.TestApps.TestBatch
{
    public class LogProcesser : IProcesser<InputLog, OutputLog>
    {
        public IEnumerable<OutputLog> Process(IEnumerable<InputLog> items)
        {
            return items.Select(Process);
        }

        public OutputLog Process(InputLog item)
        {
            var output = new OutputLog()
            {
                Id = item.Id,
                Date = item.Logged,
                Callsite = item.Callsite,
                Logger = item.Logger,
                Message = item.Message,
                Level = item.Level
            };

            return output;
        }
    }
}
