using System;
using Dragonfly.Core.Readers;
using Dragonfly.FlatFileHelper;

namespace Dragonfly.TestApps.TestFileOperations
{
    public static class TestRead
    {
        private const string ProductsFile = @"D:\Temp\Northwind.Products.csv";

        public static void DoTests()
        {
            ReadFile();
            ReadFile(10);
            ReadFile(20, 10);
        }

        private static void ReadFile(int skip = 0, int batch = int.MaxValue)
        {
            var configuration = new DefaultReaderConfiguration
                {
                    ResourceName = @"D:\Temp\Northwind.Products.csv",
                    RowsToSkip = skip,
                    BatchSize = batch,
                    HasHeaders = true,
                    ReturnHeaders = true
                };
            var reader = new FlatFileReader(configuration);
            foreach (var row in reader.GetRows())
            {
                Console.WriteLine($"Row Info: {row.Number} => {row.Row}");
            }
            Console.Write("Press [Enter] to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
