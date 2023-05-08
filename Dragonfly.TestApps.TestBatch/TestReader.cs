using System;
using Dragonfly.Batch;
using Dragonfly.Batch.Processer;
using Dragonfly.FlatFileHelper;

namespace Dragonfly.TestApps.TestBatch
{
    public static class TestReader
    {
        public static void DoTest0()
        {
            using (var reader = new FlatFileReader()
                                    .FileName(@"D:\Temp\Products.csv")
                                    .HasHeaders(true))
            {
                var csvReader = new CsvReader<Product>(reader, new CsvParser<Product>(new ParserConfiguration {Delimiter = ";", HasHeader = true}));
                foreach (var product in csvReader.Read(10,20))
                {
                    Console.WriteLine($"Id: {product.ProductId}, Category: {product.CategoryName}, Name: {product.ProductName}");
                }
            }
        }

        public static void DoTest1()
        {
            var reader = new FlatFileReader()
                .FileName(@"D:\Temp\Products.csv")
                .HasHeaders(true);

            var parser = new ProductParser();
            var csvReader = new CsvReader<Product>(reader, parser);

            new DataFlow<Product, Product>()
                .SetReader(csvReader)
                .SetProcesser(new CastProcesser<Product, Product>())
                .SetWriter(new ProductWriter())
                .OnError()
                .Report(exception => {Console.WriteLine(exception.Message);})
                .AndThen()
                .Continue()
                .Execute();
        }

        public static void DoTest2()
        {
            var reader = new FlatFileReader()
                .FileName(@"D:\GML\Dragonfly\Test Files\Products.Csv")
                .HasHeaders(true);
            while (reader.Read())
            {
                var rowInfo = reader.GetRow();
                Console.WriteLine($"Row: {rowInfo.Number}, Data: {rowInfo.Row}");
            }
        }
    }
}
