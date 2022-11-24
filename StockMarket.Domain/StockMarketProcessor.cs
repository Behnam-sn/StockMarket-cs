namespace StockMarket.Domain
{
    public abstract class BaseComparer : IComparer<Order>
    {
        public int Compare(Order? x, Order? y)
        {
            var result = SpecificCompare(x, y);
            if (result != 0) return result;

            if (y.Id < x.Id) return 1;
            if (y.Id > x.Id) return -1;

            return 0;
        }

        protected abstract int SpecificCompare(Order x, Order y);
    }
    public class MaxComparer : BaseComparer
    {
        protected override int SpecificCompare(Order x, Order y)
        {
            if (y.Price > x.Price) return 1;
            if (y.Price < x.Price) return -1;
            return 0;
        }
    }
    public class MinComparer : BaseComparer
    {
        protected override int SpecificCompare(Order x, Order y)
        {
            if (y.Price < x.Price) return 1;
            if (y.Price > x.Price) return -1;
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