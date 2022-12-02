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

            return matchOrder(
                order: order,
                orders: buyOrders,
                matchingOrders: sellOrders,
                comparePriceDelegate: (decimal price1, decimal price2) => price1 <= price2
                );
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

            return matchOrder(
                order: order,
                orders: sellOrders,
                matchingOrders: buyOrders,
                comparePriceDelegate: (decimal price1, decimal price2) => price1 >= price2
                );
        }
        private long matchOrder(Order order, PriorityQueue<Order, Order> orders, PriorityQueue<Order, Order> matchingOrders, Func<decimal, decimal, bool> comparePriceDelegate)
        {
            while ((matchingOrders.Count > 0) && (order.Quantity > 0) && comparePriceDelegate(matchingOrders.Peek().Price, order.Price))
            {
                var peekedOrder = matchingOrders.Peek();
                makeTrade(peekedOrder, order);
                if (peekedOrder.Quantity == 0) matchingOrders.Dequeue();
            }

            if (order.Quantity > 0) orders.Enqueue(order, order);

            return order.Id;
        }
        private void makeTrade(Order order1, Order order2)
        {
            var matchingOrders = findOrders(order1, order2);
            var minQuantity = Math.Min(matchingOrders.SellOrder.Quantity, matchingOrders.BuyOrder.Quantity);
            Interlocked.Increment(ref lastTradeNumber);

            trades.Add(new Trade(
                id: lastTradeNumber,
                sellOrderId: matchingOrders.SellOrder.Id,
                buyOrderId: matchingOrders.BuyOrder.Id,
                price: matchingOrders.SellOrder.Price,
                quantity: minQuantity
                ));

            matchingOrders.SellOrder.DecreaseQuantity(minQuantity);
            matchingOrders.BuyOrder.DecreaseQuantity(minQuantity);
        }
        private static (Order SellOrder, Order BuyOrder) findOrders(Order order1, Order order2)
        {
            if (order1.Side == TradeSide.Sell) return (SellOrder: order1, BuyOrder: order2);
            return (SellOrder: order2, BuyOrder: order1);

        }
    }
}