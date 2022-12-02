namespace StockMarket.Domain
{
    public class StockMarketProcessor
    {
        private MarketState state;
        private long lastOrderNumber;
        private long lastTradeNumber;
        private List<Trade> trades;
        private PriorityQueue<Order, Order> buyOrders;
        private PriorityQueue<Order, Order> sellOrders;
        public IEnumerable<Trade> Trades => trades;

        public StockMarketProcessor(long lastOrderNumber = 0, long lastTradeNumber = 0)
        {
            this.lastOrderNumber = lastOrderNumber;
            this.lastTradeNumber = lastOrderNumber;
            trades = new List<Trade>();
            buyOrders = new PriorityQueue<Order, Order>(new MaxComparer());
            sellOrders = new PriorityQueue<Order, Order>(new MinComparer());
        }
        public void Open()
        {
            state = MarketState.Open;
        }
        public long EnqueueOrder(TradeSide side, decimal price, decimal quantity)
        {
            if (side == TradeSide.Buy) return processBuyOrder(side, price, quantity);
            return processSellOrder(side, price, quantity);
        }
        private long processBuyOrder(TradeSide side, decimal price, decimal quantity)
        {
            Interlocked.Increment(ref lastOrderNumber);
            var order = new Order(
                id: lastOrderNumber,
                side: side,
                price: price,
                quantity: quantity
                );

            while ((sellOrders.Count > 0) && (order.Quantity > 0) && (sellOrders.Peek().Price <= order.Price))
            {
                var sellOrder = sellOrders.Peek();
                makeTrade(
                    sellOrder: sellOrder,
                    buyOrder: order
                    );

                if (sellOrder.Quantity == 0) sellOrders.Dequeue();
            }

            if (order.Quantity > 0) buyOrders.Enqueue(order, order);

            return order.Id;
        }
        private long processSellOrder(TradeSide side, decimal price, decimal quantity)
        {
            Interlocked.Increment(ref lastOrderNumber);
            var order = new Order(
                id: lastOrderNumber,
                side: side,
                price: price,
                quantity: quantity
                );

            while ((buyOrders.Count > 0) && (order.Quantity > 0) && (buyOrders.Peek().Price >= order.Price))
            {
                var buyOrder = buyOrders.Peek();
                makeTrade(
                    sellOrder: order,
                    buyOrder: buyOrder
                    );

                if (buyOrder.Quantity == 0) buyOrders.Dequeue();
            }

            if (order.Quantity > 0) sellOrders.Enqueue(order, order);

            return order.Id;
        }
        private void makeTrade(Order sellOrder, Order buyOrder)
        {
            Interlocked.Increment(ref lastTradeNumber);

            var minQuantity = Math.Min(sellOrder.Quantity, buyOrder.Quantity);
            trades.Add(new Trade(
                id: lastTradeNumber,
                sellOrderId: sellOrder.Id,
                buyOrderId: buyOrder.Id,
                price: sellOrder.Price,
                quantity: minQuantity
                ));

            sellOrder.DecreaseQuantity(minQuantity);
            buyOrder.DecreaseQuantity(minQuantity);
        }
    }
}