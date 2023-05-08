using System;
using System.Collections.Generic;
using System.Linq;
using Dragonfly.Batch.Filters;
using Dragonfly.Core;

namespace Dragonfly.TestApps.TestBatch
{
    public class PreFilter : IFilter<InputLog>
    {
        //private int _count;
        private readonly DateTime _begin = DateTime.Parse("2017-02-08T18:48:00");
        private readonly DateTime _end = DateTime.Parse("2017-02-08 18:50:00");
        private readonly Action<InputLog> _action;

        public PreFilter(Action<InputLog> action = null)
        {
            _action = action;
        }

        public InputLog Filter(InputLog input)
        {
            var ok = input.Level == "Error";
            //var ok = (input.Level == "Error" && input.Logged.IsBetween(_begin, _end, true));
            if (ok) return input;

            _action?.Invoke(input);
            return null;
        }

        public IEnumerable<InputLog> Filter(IEnumerable<InputLog> input)
        {
            return input.Select(Filter);
        }
    }
}
