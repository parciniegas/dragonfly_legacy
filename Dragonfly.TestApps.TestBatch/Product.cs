using FileHelpers;

namespace Dragonfly.TestApps.TestBatch
{
    [DelimitedRecord(";")]
    [IgnoreFirst]
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CompanyName { get; set; }
        public string CategoryName{ get; set; }
        public string QuantityPerUnit{ get; set; }
        public decimal UnitPriceDecimal { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public int Reorderlevel { get; set; }
        public bool Discontinued { get; set; }
    }
}
