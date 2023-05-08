using System;
using System.Collections.Generic;
using Dragonfly.Core.Writers;

namespace Dragonfly.TestApps.TestBatch
{
    public class ProductWriter : IWriter<Product>
    {
        public void Write(Product item)
        {
            Console.WriteLine($"Id: {item.ProductId}, Category: {item.CategoryName}, Name: {item.ProductName}");
        }

        public void Write(IEnumerable<Product> items)
        {
            throw new NotImplementedException();
        }
    }
}
