using System;

namespace Dragonfly.TestApps.TestBatch
{
    public class InputLog
    {
        public int Id { get; set; }
        public DateTime Logged { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public bool? Https { get; set; }
        public string Logger { get; set; }
        public string Callsite { get; set; }
    }
}
