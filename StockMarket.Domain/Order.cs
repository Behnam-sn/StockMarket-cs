namespace StockMarket.Domain
{
    public class Order
    {
        public long Id { get; internal set; }
        public decimal Price { get; internal set; }
    }
}