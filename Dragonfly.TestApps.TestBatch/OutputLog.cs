using System;

namespace Dragonfly.TestApps.TestBatch
{
    public class OutputLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Callsite { get; set; }
    }
}
