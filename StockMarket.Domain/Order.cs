namespace StockMarket.Domain
{
    public class Order
    {
        public long Id { get; }
        public TradeSide Side { get; }
        public decimal Price { get; }
        public decimal Quantity { get; internal set; }

        internal Order(long id, TradeSide side, decimal price, decimal quantity)
        {
            Id = id;
            Side = side;
            Price = price;
            Quantity = quantity;
        }
        internal void DecreaseQuantity(decimal minQuantity)
        {
            Quantity -= minQuantity;
        }
    }
}