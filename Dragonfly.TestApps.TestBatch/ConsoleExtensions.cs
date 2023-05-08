using System;

namespace Dragonfly.TestApps.TestBatch
{
    public static class ConsoleExtensions
    {
        public static void WriteAt(int column, int row, string text)
        {
            Console.CursorLeft = column;
            Console.CursorTop = row;
            Console.Write(text);
        }
    }
}
