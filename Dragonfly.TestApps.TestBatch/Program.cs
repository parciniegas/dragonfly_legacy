using System;

namespace Dragonfly.TestApps.TestBatch
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Monitor.Start();
            //new TestLog().DoTest1();
            //new TestLog().DoTest2();
            //new TestLog().DoTest3();
            //new TestLog().DoTest4();
            //new TestLog().DoTest5();
            //TestReader.DoTest1();
            //TestReader.DoTest2();
            //TestFileHelpers.DoTest1();
            TestReader.DoTest1();
            Console.WriteLine();
            Monitor.Stop();
            Console.Write("Press [Enter] to continue...");
            Console.ReadLine();
        }
    }
}
