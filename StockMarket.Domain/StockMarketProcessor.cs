namespace StockMarket.Domain
{
    public class StockMarketProcessor
    {
        private MarketState state;
        private PriorityQueue<Order, Order> buyOrders;
        private PriorityQueue<Order, Order> sellOrders;
        public StockMarketProcessor()
        {
            buyOrders = new PriorityQueue<Order, Order>(new MaxComparer());
            sellOrders = new PriorityQueue<Order, Order>(new MinComparer());
        }
        public void Open()
        {
            state = MarketState.Open;
        }
        public void EnqueueOrder(TradeSide side, decimal price, decimal quantity)
        {
            throw new NotImplementedException();
        }
    }
}