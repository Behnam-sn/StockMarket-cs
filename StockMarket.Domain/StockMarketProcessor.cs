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
            if (side == TradeSide.Buy)
            {
                processBuyOrder(side, price, quantity);
            }
            else
            {
                processSellOrder(side, price, quantity);
            }
        }

        private void processBuyOrder(TradeSide side, decimal price, decimal quantity)
        {
            throw new NotImplementedException();
        }

        private void processSellOrder(TradeSide side, decimal price, decimal quantity)
        {
            throw new NotImplementedException();
        }
    }
}