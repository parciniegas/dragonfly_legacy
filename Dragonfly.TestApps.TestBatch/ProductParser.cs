using Dragonfly.Core;
using Dragonfly.Core.Readers;

namespace Dragonfly.TestApps.TestBatch
{
    public class ProductParser : IParser<Product>
    {
        public RecordInfo<Product> ParseRow(RowInfo row)
        {
            var fields = row.Row.Split(';');
            var product = new Product
            {
                ProductId = fields[0].ToInt(),
                ProductName = fields[1],
                CompanyName = fields[2],
                CategoryName = fields[3]
            };

            return new RecordInfo<Product>
            {
                Number = row.Number,
                Record = product
            };
        }

        public void SetHeaders(RowInfo row)
        {
            return;
        }
    }
}
