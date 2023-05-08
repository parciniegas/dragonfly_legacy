using System;
using System.Diagnostics;

namespace Dragonfly.Validator
{
    public class TraceLogger : ILogger
    {
        public void Debug(object message)
        {
            Trace.WriteLine(message);
        }

        public void Error(Exception ex)
        {
            Trace.WriteLine(ex.StackTrace);
        }

        public void Error(object message, Exception ex)
        {
            Trace.WriteLine(message);
            Trace.WriteLine(ex.StackTrace);
        }
    }
}