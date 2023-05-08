using System;
using System.Diagnostics;

namespace Dragonfly.TestApps.TestBatch
{
    public static class Monitor
    {
        private static readonly Stopwatch Stopwatch = Stopwatch.StartNew();

        public static void Start()
        {
            Stopwatch.Start();
            Console.WriteLine(DateTime.Now.ToLongTimeString());
        }

        public static void Stop()
        {
            Stopwatch.Stop();
            Console.WriteLine(DateTime.Now.ToLongTimeString());
            Console.WriteLine($"{Stopwatch.Elapsed.TotalSeconds} seconds");
        }
    }
}
