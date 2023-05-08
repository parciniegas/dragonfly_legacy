using Dragonfly.Batch;
using Dragonfly.Batch.Processer;

namespace Dragonfly.TestApps.TestBatch
{
    public static class TestFileHelpers
    {
        public static void DoTest1()
        {
            new DataFlow<Product, Product>()
                .SetReader(new FileHelpersReader(@"D:\Temp\Products.csv"))
                .SetWriter(new ProductWriter())
                .SetProcesser(new CastProcesser<Product, Product>())
                .Execute();
        }
    }
}
