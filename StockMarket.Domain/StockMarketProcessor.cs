namespace StockMarket.Domain
{
    public class MaxComparer : IComparer<Order>
    {
        public int Compare(Order? x, Order? y)
        {
            if (y.Price > x.Price) return 1;
            if (y.Price < x.Price) return -1;

            if (y.Id < x.Id) return 1;
            if (y.Id > x.Id) return -1;

            return 0;
        }
    }
    public class MinComparer : IComparer<Order>
    {
        public int Compare(Order? x, Order? y)
        {
            if (y.Price < x.Price) return 1;
            if (y.Price > x.Price) return -1;

            if (y.Id < x.Id) return 1;
            if (y.Id > x.Id) return -1;

            return 0;
        }
    }
    public class StockMarketProcessor
    {
        private MarketState state;
        private PriorityQueue<Order, Order> buyOrders;
        private PriorityQueue<Order, Order> sellOrders;
        public StockMarketProcessor()
        {
            buyOrders = new PriorityQueue<Order, Order>();
            sellOrders = new PriorityQueue<Order, Order>();
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